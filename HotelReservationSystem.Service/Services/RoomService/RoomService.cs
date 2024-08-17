using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
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

        public async Task<RoomToReturnDto> AddRoomAsync(RoomDto roomDto)
        {

            var newRoom = new Room
            {
                Type = roomDto.Type,
                Status = roomDto.Status,
                Price = roomDto.Price,
                PictureUrl = DocumentSetting.UploadFile(roomDto.PictureFile, "RoomImages")
            };

            var facilities = await _unitOfWork.Repository<Facility>()
                                     .GetAsync(f => roomDto.FacilityIds.Contains(f.Id));

            foreach (var facility in facilities)
            {
                newRoom.RoomFacilities.Add(new RoomFacility
                {
                    RoomId = newRoom.Id,
                    FacilityId = facility.Id
                });
            }

            await _unitOfWork.Repository<Room>().AddAsync(newRoom);
            await _unitOfWork.CompleteAsync();

            var mappedRoom = _mapper.Map<RoomToReturnDto>(newRoom);
            mappedRoom.TotalPrice = CalculateRoomTotalPrice(roomDto.Price, facilities);

            return mappedRoom;
        }

        public async Task<RoomToReturnDto> UpdateRoomAsync(int id, RoomDto roomDto)
        {

            var Spec = new RoomSpecification(id);
            var OldRoom = await _unitOfWork.Repository<Room>().GetByIdWithSpecAsync(Spec);

            OldRoom.Type = roomDto.Type;
            OldRoom.Price = roomDto.Price;
            OldRoom.Status = roomDto.Status;
            OldRoom.PictureUrl = DocumentSetting.UploadFile(roomDto.PictureFile, "RoomImages");

            var facilities = await _unitOfWork.Repository<Facility>()
                            .GetAsync(f => roomDto.FacilityIds.Contains(f.Id));

            foreach (var facility in facilities)
            {
                OldRoom.RoomFacilities.Add(new RoomFacility
                {
                    RoomId = OldRoom.Id,
                    FacilityId = facility.Id
                });
            }

            _unitOfWork.Repository<Room>().Update(OldRoom);
            await _unitOfWork.CompleteAsync();
            var mappedRoom = _mapper.Map<RoomToReturnDto>(OldRoom);
            mappedRoom.TotalPrice = CalculateRoomTotalPrice(roomDto.Price, facilities);

            return mappedRoom;

        }

        private decimal CalculateRoomTotalPrice(decimal roomPrice, IEnumerable<Facility> facilities)
        {
            var facilitiesPrice = facilities.Sum(f => f.Price);

            return roomPrice + facilitiesPrice;
        }


    }
}
