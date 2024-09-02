using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.OfferServices.Dtos;

namespace HotelReservationSystem.Service.Services.OfferServices
{
    public class OfferService : IOfferService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferService(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OfferDto> AddOfferAsync(OfferDto offerDto)
        {

            var newOffer = new Offer
            {
                StartDate = offerDto.StartDate,
                EndDate = offerDto.EndDate,
                Discount = offerDto.Discount
            };

            await _unitOfWork.Repository<Offer>().AddAsync(newOffer);
            await _unitOfWork.SaveChangesAsync();
            var mappedOffer = _mapper.Map<OfferDto>(newOffer);

            return mappedOffer;
        }

        public async Task<OfferDto> UpdateOfferAsync(int id, OfferToReturnDto offerDto)
        {
            var OldOffer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id);
            if (OldOffer == null) return null;

            OldOffer.StartDate = offerDto.StartDate;
            OldOffer.EndDate = offerDto.EndDate;
            OldOffer.Discount = offerDto.Discount;


            _unitOfWork.Repository<Offer>().Update(OldOffer);
            await _unitOfWork.SaveChangesAsync();
            var mappedOffer = _mapper.Map<OfferDto>(OldOffer);

            return mappedOffer;

        }

        public async Task<IEnumerable<OfferToReturnDto>> GetAllOffersAsync()
        {
            var offers = await _unitOfWork.Repository<Offer>().GetAllAsync();
            var mappedOffers = _mapper.Map<IEnumerable<OfferToReturnDto>>(offers);
            return mappedOffers;
        }
        public async Task<decimal> ApplyOfferAsync(int offerId, decimal amount)
        {
            var offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(offerId);
            if (offer == null || DateTime.Now < offer.StartDate || DateTime.Now > offer.EndDate)
            {
                return amount; 
            }

            var discountedAmount = amount - (amount * offer.Discount / 100);
            return discountedAmount;
        }


        public async Task<bool> DeleteOfferAsync(int id)
        {
            var Offer = await _unitOfWork.Repository<Offer>().GetByIdAsync(id);
            if (Offer == null) return false;

            _unitOfWork.Repository<Offer>().Delete(Offer);
            var Result = await _unitOfWork.SaveChangesAsync();

            return Result > 0;
        }


    }
}
