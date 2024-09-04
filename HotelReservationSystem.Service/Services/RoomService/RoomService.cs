using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.AspNetCore.Http;

namespace HotelReservationSystem.Service.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IunitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Room> AddRoomAsync(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);

            await _unitOfWork.Repository<Room>().AddAsync(room);

            await _unitOfWork.SaveChangesAsync();
            return room;
        }
        public async Task<bool> AddPicturesToRoomAsync(List<IFormFile> pictureUrls, int roomId)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);
            if (room == null)
                return false;

            foreach (var file in pictureUrls)
            {
                var fileName = DocumentSetting.UploadFile(file, "RoomImages");

                var picture = new Picture
                {
                    Url = fileName,
                    RoomId = roomId,
                };

                await _unitOfWork.Repository<Picture>().AddAsync(picture);
            }

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Room> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var spec = new RoomSpecification(id);
            var room = await _unitOfWork.Repository<Room>().GetByIdWithSpecAsync(spec);
            if (room == null)
                return null;

            _mapper.Map(roomDto, room); 

            _unitOfWork.Repository<Room>().Update(room);
            await _unitOfWork.SaveChangesAsync();
            return room;
        }
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var spec = new RoomSpecification(id);
            var room = await _unitOfWork.Repository<Room>().GetByIdWithSpecAsync(spec);
            if (room == null)
                return false;

            foreach (var picture in room.PictureUrls)
            {
                DocumentSetting.DeleteFile("RoomImages", Path.GetFileName(picture.Url));
                _unitOfWork.Repository<Picture>().Delete(picture);
            }

            _unitOfWork.Repository<Room>().Delete(room);

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0; 
        }

        public async Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync(SpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate)
        {
            var spec = new RoomAvailabilitySpecification(roomSpec, checkInDate, checkOutDate);

            var rooms = await _unitOfWork.Repository<Room>().GetAllWithSpecAsync(spec);

            var roomDtos = _mapper.Map<IEnumerable<RoomToReturnDto>>(rooms);

            return roomDtos;
        }
        public async Task<int> GetAvailableRoomCount(DateTime checkInDate, DateTime checkOutDate)
        {
            var spec = new CountRoomWithSpec(checkInDate, checkOutDate);
            var count = await _unitOfWork.Repository<Room>().GetCountWithSpecAsync(spec);
            return count;
        }

        public async Task<RoomToReturnDto> GetRoomByIDAsync(int id)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(id);
            var roomDto= _mapper.Map<RoomToReturnDto>(room);
            return roomDto;
        }

        public async Task<decimal> GetRoomPriceAsync(int id)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(id);

            return room.Price;
        }
    }
}
