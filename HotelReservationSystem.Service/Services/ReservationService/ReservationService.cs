

using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public class ReservationService: IReservationService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(
            IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public decimal CalculaterReservationTotalCost(decimal roomPrice,Reservation reservation)
        {
            var stayingDays = (decimal)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays;
            return stayingDays * roomPrice;
        }
       public async Task<Reservation> UpdateReservationAsync(int id, ReservationDto reservation,decimal roomPrice)
        {
            var oldReservation =  await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
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
    }
}
