using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Repository;
using HotelReservationSystem.Service.Services.RoomFacilityService;
using HotelReservationSystem.Service.Services.RoomService;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

namespace HotelReservationSystem.Mediator.RoomMediator
{
    public class RoomMediator : IRoomMediator
    {
        private readonly IRoomService _roomService;
        private readonly IRoomFacilityService _roomFacilityService;
        private readonly IMapper _mapper;

        public RoomMediator(
            IRoomService roomService,
            IRoomFacilityService roomFacilityService,
            IMapper mapper)
        {
            _roomService = roomService;
            _roomFacilityService = roomFacilityService;
            _mapper = mapper;
        }
        public async Task<RoomToReturnDto> AddRoomAsync(RoomDto roomDto)
        {
            var newRoom = await _roomService.AddRoomAsync(roomDto);

            await _roomFacilityService.AddOrUpdateFacilitiesToRoomAsync(newRoom.Id, roomDto.FacilityIds);

            var mappedRoom = _mapper.Map<RoomToReturnDto>(newRoom);

            return mappedRoom;
        }

        public async Task<RoomToReturnDto> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(id, roomDto);

            await _roomFacilityService.AddOrUpdateFacilitiesToRoomAsync(updatedRoom.Id, roomDto.FacilityIds);

            var mappedRoom = _mapper.Map<RoomToReturnDto>(updatedRoom);

            return mappedRoom;
        }


        //private decimal CalculateRoomTotalPrice(decimal roomPrice, IEnumerable<Facility> facilities)
        //{

        //    var facilitiesPrice = facilities.Sum(f => f.Price);

        //    return roomPrice + facilitiesPrice;
        //}
    }
}
