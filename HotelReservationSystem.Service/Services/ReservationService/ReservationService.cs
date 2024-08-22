using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.ReservationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Reservation> MakeReservationAsync(ReservationDto request)
        {
            if (request.CheckInDate >= request.CheckOutDate)
                throw new ArgumentException("Check-out date must be later than check-in date.");

            var roomRepo = _unitOfWork.Repository<Room>();
            var room = await roomRepo.GetByIdAsync(request.RoomId);

            if (room == null)
                throw new ArgumentException($"Room with ID {request.RoomId} does not exist.");

            var conflictingReservations = await _unitOfWork.Repository<Reservation>().GetAsync(r =>
                r.RoomId == request.RoomId &&
                r.CheckInDate < request.CheckOutDate &&
                r.CheckOutDate > request.CheckInDate  
               //!r.Status.HasFlag(ReservationStatus.Cancelled)
                );

            var isRoomAvailable = !conflictingReservations.Any();
            if (!isRoomAvailable)
                throw new InvalidOperationException("Room is not available for the selected dates.");

            var reservation = _mapper.Map<Reservation>(request);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();

            return reservation;

        }
        public async Task UpdateReservationStatusAsync(int reservationId, ReservationStatus newStatus)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);

            if (reservation == null)
                throw new ArgumentException($"Reservation with ID {reservationId} does not exist.");

            reservation.Status = newStatus;
            await _unitOfWork.CompleteAsync();
        }
    }
}
