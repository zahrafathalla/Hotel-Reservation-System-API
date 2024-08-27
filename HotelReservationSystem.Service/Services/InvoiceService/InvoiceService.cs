using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.ReservationSpecifications;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;

namespace HotelReservationSystem.Service.Services.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        IunitOfWork _unitOfWork;
        IMapper _mapper;
        public InvoiceService(IunitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InvoiceToReturnDto>> GetAllAsync()
        {
            var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            var mappedInvoice = _mapper.Map<IEnumerable<InvoiceToReturnDto>>(invoices);
            return mappedInvoice;         
        }
        public async Task<InvoiceToReturnDto> GenerateInvoiceAsync(int reservationId)
        {

            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);

            //result object with default failure flag
            var result = new InvoiceToReturnDto
            {
                IsSuccessful = false, 

            };

            if (reservation == null || reservation.Status == ReservationStatus.Pending || reservation.Status == ReservationStatus.Cancelled)
                return result;


            var TotalAmount = reservation.TotalAmount;

            var newInvoic = new Invoice()
            {
                Amount = TotalAmount,
                InvoiceDate = DateTime.Now,
                ReservationId = reservationId
            };

            await _unitOfWork.Repository<Invoice>().AddAsync(newInvoic);

            await _unitOfWork.CompleteAsync();

            var mappedInvoice = _mapper.Map<InvoiceToReturnDto>(newInvoic);
            return mappedInvoice;

        }
    }
}
