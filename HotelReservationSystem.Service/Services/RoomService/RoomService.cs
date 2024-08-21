using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(
            IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Room> AddRoomAsync(RoomDto roomDto)
        {

            var newRoom = _mapper.Map<Room>(roomDto);

            newRoom.PictureUrl = DocumentSetting.UploadFile(roomDto.PictureFile, "RoomImages");

            await _unitOfWork.Repository<Room>().AddAsync(newRoom);
            await _unitOfWork.CompleteAsync();

            return newRoom;
        }

        public async Task<Room> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var oldRoom = await _unitOfWork.Repository<Room>().GetByIdAsync(id);
            if (oldRoom == null)
                return null;

            _mapper.Map(roomDto, oldRoom);

            oldRoom.PictureUrl = DocumentSetting.UpdateFile(roomDto.PictureFile, "RoomImages", oldRoom.PictureUrl);

            _unitOfWork.Repository<Room>().Update(oldRoom);
            await _unitOfWork.CompleteAsync();

            return oldRoom;
        }
         
        public async Task<IEnumerable<RoomToReturnDto>> GetAllAsync(RoomSpecParams roomSpec)
        {
            var spec = new RoomSpecification(roomSpec);
            var rooms = await _unitOfWork.Repository<Room>().GetAllWithSpecAsync(spec); 
            var roomDtos = _mapper.Map<IEnumerable<RoomToReturnDto>>(rooms);
            
            return  roomDtos;
        }

        public async Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync()
        {
            var rooms = await _unitOfWork.Repository<Room>().GetAsync(R => R.Status == RoomStatus.Available);
            var roomDtos = _mapper.Map<IEnumerable<RoomToReturnDto>>(rooms);

            return roomDtos;
        }

        public async Task<bool> DeleteRoomAsync(int id)  
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(id);
            if (room == null || room.IsDeleted)
            {
                return false;
            }

            DocumentSetting.DeleteFile("RoomImages", Path.GetFileName(room.PictureUrl));
            _unitOfWork.Repository<Room>().Delete(room);
            await _unitOfWork.CompleteAsync();

            return true;
        }
        
    }
}
