using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReservationCreatedToReturnDto> MakeReservationAsync(ReservationDto reservationDto)
        {
            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
                return null;

            var roomRepo = _unitOfWork.Repository<Room>();
            var spec = new RoomSpecification(reservationDto.RoomId);
            var room = await roomRepo.GetByIdWithSpecAsync(spec);

            if (room == null)
                return null;

            var conflictingReservations = await _unitOfWork.Repository<Reservation>().GetAsync(r =>
                r.RoomId == reservationDto.RoomId &&
                r.CheckInDate < reservationDto.CheckOutDate &&
                r.CheckOutDate > reservationDto.CheckInDate &&
                r.Status != ReservationStatus.Cancelled
                );

            if (conflictingReservations.Any())
                return null;

            var reservation = _mapper.Map<Reservation>(reservationDto);

            var roomPrice = room.Price;
            decimal facilityTotalPrice = 0m;


            var roomFacilities = await _unitOfWork.Repository<RoomFacility>()
                    .GetAsync(rf => rf.RoomId == reservationDto.RoomId);


            foreach (var facilityId in reservationDto.Facilities)
            {
                if (!roomFacilities.Any(rf => rf.FacilityId == facilityId))
                {
                    var facility = await _unitOfWork.Repository<Facility>().GetByIdAsync(facilityId);
                    if (facility != null)
                    {
                        reservation.ReservationFacilities.Add(new ReservationFacility
                        {
                            ReservationId = reservation.Id,
                            FacilityId = facility.Id
                        });
                        facilityTotalPrice += facility.Price;
                    }
                }
            }
            reservation.TotalAmount = CalculaterReservationTotalCost(roomPrice + facilityTotalPrice, reservation);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();

            var mappedReservation = _mapper.Map<ReservationCreatedToReturnDto>(reservation);
            return mappedReservation;
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservationRepo = _unitOfWork.Repository<Reservation>();
            var reservation = await reservationRepo.GetByIdAsync(reservationId);

            if (reservation == null)
                return false;

            if (reservation.Status == ReservationStatus.CheckedOut || reservation.Status == ReservationStatus.Cancelled)
                return false;

            reservation.Status = ReservationStatus.Cancelled;
            reservationRepo.Update(reservation);

            await _unitOfWork.CompleteAsync();

            return true;
        }
        public async Task<Reservation> UpdateReservationAsync(int id, ReservationDto reservation, decimal roomPrice)
        {
            var oldReservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (oldReservation == null)
                return null;

            var newReservation = _mapper.Map<Reservation>(reservation);
            oldReservation.CheckInDate = newReservation.CheckInDate;
            oldReservation.CheckOutDate = newReservation.CheckOutDate;
            oldReservation.RoomId = newReservation.RoomId;
            oldReservation.TotalAmount = CalculaterReservationTotalCost(roomPrice, newReservation);
            await _unitOfWork.CompleteAsync();
            return oldReservation;
        }
        private decimal CalculaterReservationTotalCost(decimal roomPrice, Reservation reservation)
        {
            var stayingDays = (decimal)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays;
            return stayingDays * roomPrice;
        }

        public async Task UpdateReservationStatusAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>()
                                        .GetAsync(r => r.Status == ReservationStatus.CheckedIn && DateTime.Now >= r.CheckOutDate);


            foreach(var reservation in reservations)
            {
                reservation.Status = ReservationStatus.CheckedOut;
                _unitOfWork.Repository<Reservation>().Update(reservation);

            }
            await _unitOfWork.CompleteAsync();
        }
    }
}