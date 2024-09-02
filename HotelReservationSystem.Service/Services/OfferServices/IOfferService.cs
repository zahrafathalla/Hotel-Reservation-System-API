using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Service.Services.OfferServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.OfferServices
{
    public interface IOfferService
    {
        Task<OfferDto> AddOfferAsync(OfferDto offerDto);
        Task<OfferDto> UpdateOfferAsync(int id , OfferToReturnDto offerDto );
        Task<bool> DeleteOfferAsync(int id);
    }
}
