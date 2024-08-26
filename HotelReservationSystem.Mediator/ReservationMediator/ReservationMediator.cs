using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.FacilityService;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomFacilityService;
using HotelReservationSystem.Service.Services.RoomService;

namespace HotelReservationSystem.Mediator.ReservationMediator
{
    public class ReservationMediator:IReservationMediator
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IRoomFacilityService _roomFacilityService;
        private readonly IFacilityService _facilityService;
        public ReservationMediator
            (IReservationService reservationService,
            IRoomService roomService, 
            IRoomFacilityService roomFacilityService,
            IFacilityService facilityService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _roomFacilityService = roomFacilityService;
            _facilityService = facilityService;

        }
        public async Task<ReservationToReturnDto> CreateReservationAsync(ReservationDto reservationDto)
        {
            var isConflict = await _reservationService.IsReservationConflictAsync(reservationDto);
            if (isConflict)
                return null;

            var availableFacilities = await _roomFacilityService.GetFacilitiesByRoomIdAsync(reservationDto.RoomId);

            foreach (var facilityId in reservationDto.Facilities)
            {
                if (!availableFacilities.Any(f => f.Id == facilityId))
                {
                    return null; 
                }
            }

            var totalAmount = await CalculateTotalAmountAsync(reservationDto);

            var reservation = await _reservationService.MakeReservationAsync(reservationDto, totalAmount);

            var room = await _roomService.GetRoomByIDAsync(reservationDto.RoomId);

            return reservation;

        }
        public async Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationDto reservationDto)
        {

            var isConflict = await _reservationService.IsReservationConflictOnUpdateAsync(id,reservationDto);
            if (isConflict)
                return null;

            var availableFacilities = await _roomFacilityService.GetFacilitiesByRoomIdAsync(reservationDto.RoomId);

            foreach (var facilityId in reservationDto.Facilities)
            {
                if (!availableFacilities.Any(f => f.Id == facilityId))
                {
                    return null; 
                }
            }

            var totalAmount = await CalculateTotalAmountAsync(reservationDto);

            var updatedReservation = await _reservationService.UpdateReservationAsync(id, reservationDto, totalAmount);

            return updatedReservation;

        }
        private async Task<decimal> CalculateTotalAmountAsync(ReservationDto reservationDto)
        {
            var facilityTotalPrice = await _facilityService.CalculateFacilitiesPriceAsync(reservationDto.Facilities);
            var roomPrice = await _roomService.GetRoomPriceAsync(reservationDto.RoomId);
            var stayingDays = (reservationDto.CheckOutDate - reservationDto.CheckInDate).Days;
            stayingDays = stayingDays == 0 ? 1 : stayingDays;
            return stayingDays * roomPrice + facilityTotalPrice;
        }

    }
}
