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

        public RoomMediator(IRoomService roomService,IRoomFacilityService roomFacilityService,
            IMapper mapper)
        {
            _roomService = roomService;
            _roomFacilityService = roomFacilityService;
            _mapper = mapper;
        }

        public async Task<RoomcreatedToReturnDto> AddRoomAsync(RoomDto roomDto)
        {
            var newRoom = await _roomService.AddRoomAsync(roomDto);
            await _roomFacilityService.AddFacilitiesToRoomAsync(newRoom.Id, roomDto.FacilityIds);
            var mappedRoom = _mapper.Map<RoomcreatedToReturnDto>(newRoom);

            return mappedRoom;
        }
        public async Task<RoomcreatedToReturnDto> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(id, roomDto);
            await _roomFacilityService.AddFacilitiesToRoomAsync(updatedRoom.Id, roomDto.FacilityIds);
            var mappedRoom = _mapper.Map<RoomcreatedToReturnDto>(updatedRoom);

            return mappedRoom;
        }
        public async Task<bool> RemoveRoomAsync(int id)
        {
            var roomDeleted = await _roomService.DeleteRoomAsync(id);

            if (!roomDeleted)
            {
                return false;
            }

            var facilitiesRemoved = await _roomFacilityService.RemoveFacilityFromRoomAsync(id);

            return facilitiesRemoved;
        }


    }
}
