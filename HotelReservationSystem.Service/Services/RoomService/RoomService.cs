using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.RoomService.Dtos;

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

            await _unitOfWork.CompleteAsync();
            return room;
        }
        public async Task<bool> AddPicturesToRoomAsync(PictureDto pictureDto)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(pictureDto.RoomId);
            if (room == null)
                return false;

            foreach (var file in pictureDto.pictureUrls)
            {
                var fileName = DocumentSetting.UploadFile(file, "RoomImages");

                var picture = new Picture
                {
                    Url = fileName,
                    RoomId = pictureDto.RoomId,
                };

                await _unitOfWork.Repository<Picture>().AddAsync(picture);
            }

            var result = await _unitOfWork.CompleteAsync();
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
            await _unitOfWork.CompleteAsync();
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

            var result = await _unitOfWork.CompleteAsync();
            return result > 0; 
        }

        public async Task<IEnumerable<RoomToReturnDto>> GetAllRoomsIsAvaliableAsync(RoomSpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate)
        {
            var spec = new RoomAvailabilitySpecification(roomSpec, checkInDate, checkOutDate);

            var rooms = await _unitOfWork.Repository<Room>().GetAllWithSpecAsync(spec);

            var roomDtos = _mapper.Map<IEnumerable<RoomToReturnDto>>(rooms);

            return roomDtos;
        }
        public async Task<int> GetAvailableRoomCount(RoomSpecParams roomSpec, DateTime checkInDate, DateTime checkOutDate)
        {
            var spec = new RoomAvailabilitySpecification(roomSpec, checkInDate, checkOutDate);
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
