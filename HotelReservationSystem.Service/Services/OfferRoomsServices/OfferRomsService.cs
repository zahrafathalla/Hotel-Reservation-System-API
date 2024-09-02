using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.OfferRoomsServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.OfferRoomsServices
{
    public class OfferRomsService : IOfferRooms
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferRomsService(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> AddOfferRoomAsync(OfferRoomsDto offerRoomsDto)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(offerRoomsDto.RoomId);
            var Offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(offerRoomsDto.OfferId);
            if (room == null || Offer == null) return false;

            var OfferRoom = new OfferRoom
            {
                OfferId = Offer.Id,
                RoomId = room.Id
            };

            await _unitOfWork.Repository<OfferRoom>().AddAsync(OfferRoom);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<OfferRoomsDto> UpdateOfferRoomAsync(int id, OfferRoomsDto offerRoomsDto)
        {
            var OldOfferRoom = await _unitOfWork.Repository<OfferRoom>().GetByIdAsync(id);
            OldOfferRoom.OfferId = offerRoomsDto.OfferId;
            OldOfferRoom.RoomId = offerRoomsDto.RoomId;
            if (OldOfferRoom == null) return null;

            _unitOfWork.Repository<OfferRoom>().Update(OldOfferRoom);
            await _unitOfWork.SaveChangesAsync();
            var mappedOfferRooms = _mapper.Map<OfferRoomsDto>(OldOfferRoom);

            return mappedOfferRooms;


        }
        public async Task<bool> DeleteOfferAsync(int id)
        {
            var OfferRoom = await _unitOfWork.Repository<OfferRoom>().GetByIdAsync(id);
            if (OfferRoom == null) return false;

            _unitOfWork.Repository<OfferRoom>().Delete(OfferRoom);
            var Result = await _unitOfWork.SaveChangesAsync();

            return Result > 0;
        }


    }
}
