using HotelReservationSystem.Service.Services.OfferRoomsServices.Dtos;
using HotelReservationSystem.Service.Services.OfferServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.OfferRoomsServices
{
    public interface IOfferRooms
    {
        Task<bool> AddOfferRoomAsync(OfferRoomsDto offerRoomsDto);
        Task<OfferRoomsDto> UpdateOfferRoomAsync(int id, OfferRoomsDto offerRoomsDto);
        Task<bool> DeleteOfferAsync(int id);
    }
}
