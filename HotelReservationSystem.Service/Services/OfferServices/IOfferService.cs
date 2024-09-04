using HotelReservationSystem.Service.Services.OfferServices.Dtos;

namespace HotelReservationSystem.Service.Services.OfferServices
{
    public interface IOfferService
    {
        Task<OfferToReturnDto> AddOfferAsync(OfferDto offerDto);
        Task<OfferToReturnDto> UpdateOfferAsync(int id , OfferDto offerDto );
        Task<bool> DeleteOfferAsync(int id);
        Task<IEnumerable<OfferToReturnDto>> GetAllOffersAsync();
        Task<decimal> ApplyOfferAsync(int offerId, decimal amount);
    }
}
