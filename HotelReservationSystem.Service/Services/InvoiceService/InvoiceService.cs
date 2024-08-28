using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.Facilitypecifications;
using HotelReservationSystem.Repository.Specification.FacilitySpecifications;
using HotelReservationSystem.Repository.Specification.InvoicSpecifications;
using HotelReservationSystem.Repository.Specification.ReservationSpecifications;
using HotelReservationSystem.Repository.Specification.RoomSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.FacilityService.Dtos;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;



namespace HotelReservationSystem.Service.Services.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

       
        public InvoiceService(IunitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }


        public async Task<InvoiceToReturnDto> GenerateInvoiceAsync(int reservationId)
        {

            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);

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
        public async Task<InvoiceToReturnDto> UpdateInvoicAsync(int id , InvoiceDto invoiceDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(invoiceDto.ReservationId);
            var TotalAmount = reservation.TotalAmount;

            var Spec = new InvoicSpec(id);
            var OldInvoic = await _unitOfWork.Repository<Invoice>().GetByIdWithSpecAsync(Spec);
            if (OldInvoic == null) return null;

            OldInvoic.InvoiceDate = invoiceDto.InvoiceDate;
            OldInvoic.ReservationId= invoiceDto.ReservationId;
            OldInvoic.Amount = TotalAmount;



            _unitOfWork.Repository<Invoice>().Update(OldInvoic);
            await _unitOfWork.CompleteAsync();
            var mappedInvoic = _mapper.Map<InvoiceToReturnDto>(OldInvoic);

            return mappedInvoic;
        }
        public async Task<IEnumerable<InvoiceToReturnDto>> GetAllAsync(SpecParams Params)
        {
            var spec = new InvoicSpec(Params);
            var Invoice = await _unitOfWork.Repository<Invoice>().GetAllWithSpecAsync(spec);
            var InvoiceMapped = _mapper.Map<IEnumerable<InvoiceToReturnDto>>(Invoice);

            return InvoiceMapped;
        }
        public async Task<InvoiceToReturnDto> GetInvoicByIdAsync(int id)
        {
            var spec = new InvoicSpec(id);
            var Invoic = await _unitOfWork.Repository<Invoice>().GetByIdWithSpecAsync(spec);
            var InvoicMapped = _mapper.Map<InvoiceToReturnDto>(Invoic);

            return InvoicMapped;
        }
        public async Task<int> GetCount(SpecParams Spec)
        {
            var CountInvoive = new CountInvoicWithSpec(Spec);
            var Count = await _unitOfWork.Repository<Invoice>().GetCountWithSpecAsync(CountInvoive);
            return Count;
        }
        public async Task<bool> DeleteInvoicAsync(int id)
        {
            var Invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
            if (Invoice == null) return false;

            _unitOfWork.Repository<Invoice>().Delete(Invoice);
            var Result = await _unitOfWork.CompleteAsync();

            return Result > 0;
        }



    }
}
