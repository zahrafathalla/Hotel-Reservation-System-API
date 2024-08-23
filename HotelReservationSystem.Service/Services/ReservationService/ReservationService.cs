using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
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

        public async Task<ReservationToReturnDto> MakeReservationAsync(ReservationDto reservationDto)
        {
            if (reservationDto.CheckInDate >= reservationDto.CheckOutDate)
                return null;

            var roomRepo = _unitOfWork.Repository<Room>();
            var room = await roomRepo.GetByIdAsync(reservationDto.RoomId);

            if (room == null)
                return null;

            var conflictingReservations = await _unitOfWork.Repository<Reservation>().GetAsync(r =>
                r.RoomId == reservationDto.RoomId &&
                r.CheckInDate < reservationDto.CheckOutDate &&
                r.CheckOutDate > reservationDto.CheckInDate
                );

            if (conflictingReservations.Any())
                return null; 
            

            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.CompleteAsync();

            var mappedReservation = _mapper.Map<ReservationToReturnDto>(reservation);
            return mappedReservation;
        }
    }
}
