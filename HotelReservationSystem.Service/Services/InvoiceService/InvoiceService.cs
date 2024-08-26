using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Service.Services.InvoiceService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
          var invoicesDto = _mapper.Map<IEnumerable<InvoiceToReturnDto>>(invoices);
            return invoicesDto;
            
        }
    }
}
