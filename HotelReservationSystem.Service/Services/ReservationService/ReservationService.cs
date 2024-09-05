using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification;
using HotelReservationSystem.Repository.Specification.FeedbackSpecifications;
using HotelReservationSystem.Repository.Specification.ReservationSpecifications;
using HotelReservationSystem.Repository.Specification.Specifications;
using HotelReservationSystem.Service.Services.ReservationService.Dtos;
using HotelReservationSystem.Service.Services.RoomService.Dtos;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Collections.Generic;

namespace HotelReservationSystem.Service.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> IsReservationConflictAsync(ReservationDto reservationDto)
        {
            var conflictingReservations = await _unitOfWork.Repository<Reservation>().GetAsync(r =>
                r.RoomId == reservationDto.RoomId &&
                r.CheckInDate < reservationDto.CheckOutDate &&
                r.CheckOutDate > reservationDto.CheckInDate &&
                r.Status != ReservationStatus.Cancelled &&
                r.Status != ReservationStatus.CheckedOut
                );

            return conflictingReservations.Any();
        }

        public async Task<ReservationToReturnDto> MakeReservationAsync(ReservationDto reservationDto, decimal totalAmount)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.TotalAmount = totalAmount;

            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            var mappedReservation = _mapper.Map<ReservationToReturnDto>(reservation);
            return mappedReservation;
        }

        public async Task<ReservationToReturnDto> ViewReservationDetailsAsync(int reservationId)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);
            var mappedReservation = _mapper.Map<ReservationToReturnDto>(reservation);
            return mappedReservation;
        }
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservationRepo = _unitOfWork.Repository<Reservation>();
            var reservation = await reservationRepo.GetByIdAsync(reservationId);

            if (reservation == null)
                return false;

            if (reservation.Status == ReservationStatus.CheckedOut || reservation.Status == ReservationStatus.Cancelled)
                return false;

            reservation.Status = ReservationStatus.Cancelled;
            reservationRepo.Update(reservation);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<bool> IsReservationConflictOnUpdateAsync(int reservationId, ReservationUpdatedDto reservationDto)
        {
            var existingReservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);
            if (existingReservation == null)
                return false;

            bool roomChanged = existingReservation.RoomId != reservationDto.RoomId;
            bool datesChanged = existingReservation.CheckInDate != reservationDto.CheckInDate ||
                                existingReservation.CheckOutDate != reservationDto.CheckOutDate;

            if (!roomChanged && !datesChanged)
                return false;

            int roomToCheck = roomChanged ? reservationDto.RoomId : existingReservation.RoomId;

            var conflictingReservations = await _unitOfWork.Repository<Reservation>().GetAsync(r =>
                r.RoomId == roomToCheck &&
                r.CheckInDate < reservationDto.CheckOutDate &&
                r.CheckOutDate > reservationDto.CheckInDate &&
                r.Status != ReservationStatus.Cancelled &&
                r.Status != ReservationStatus.CheckedOut &&
                r.Id != reservationId 
            );

            return conflictingReservations.Any();
        }
        public async Task<ReservationToReturnDto> UpdateReservationAsync(int id, ReservationUpdatedDto reservationDto, decimal totalAmount)
        {
            var oldReservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (oldReservation == null)
                return null;

            _mapper.Map(reservationDto, oldReservation);
            oldReservation.TotalAmount = totalAmount;

            _unitOfWork.Repository<Reservation>().Update(oldReservation);
            await _unitOfWork.SaveChangesAsync();

            var mappedReservation = _mapper.Map<ReservationToReturnDto>(oldReservation);
            return mappedReservation;
        }

        public async Task UpdateCheckInStatusesAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>()
                        .GetAsync(r => r.Status == ReservationStatus.PaymentReceived &&
                                       r.CheckInDate.Date <= DateTime.Now.Date);

            foreach (var reservation in reservations)
            {
                reservation.Status = ReservationStatus.CheckedIn;
                _unitOfWork.Repository<Reservation>().Update(reservation);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCheckOutStatusesAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>()
                      .GetAsync(r => r.Status == ReservationStatus.CheckedIn &&
                                     r.CheckOutDate.Date <= DateTime.Now.Date);

            foreach (var reservation in reservations)
            {
                reservation.Status = ReservationStatus.CheckedOut;
                _unitOfWork.Repository<Reservation>().Update(reservation);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<BookingReport>> GetAllReservationForBookingReport(SpecParams Params,DateTime firstDate, DateTime secondDate)
        {
            var spec = new ReservationSpecificationForReporting(Params, firstDate, secondDate);

            var reservations =await _unitOfWork.Repository<Reservation>()
                                 .GetAllWithSpecAsync(spec);

            var mappedReservations = _mapper.Map<IEnumerable<BookingReport>>(reservations);
            return mappedReservations;

        }
        public async Task<IEnumerable<RevenueReport>> GetAllReservationForRevenueReport(SpecParams Params, DateTime firstDate, DateTime secondDate)
        {
            var spec = new ReservationSpecificationForRevenueReport(Params, firstDate, secondDate);

            var reservations = await _unitOfWork.Repository<Reservation>()
                                  .GetAllWithSpecAsync(spec);

            var mappedReservations = _mapper.Map<IEnumerable<RevenueReport>>(reservations);
            return mappedReservations;
        }
        public async Task<IEnumerable<CustomerReport>> GetAllReservationForCustomerReport(SpecParams Params,int customerID, DateTime firstDate, DateTime secondDate)
        {
            var spec = new ReservationSpecificationForCustomerReporting(Params, firstDate, secondDate, customerID);

            var reservations =await _unitOfWork.Repository<Reservation>()
                                 .GetAllWithSpecAsync(spec);

            var mappedReservations = _mapper.Map<IEnumerable<CustomerReport>>(reservations);
            return mappedReservations;
        }

        public async Task<int> GetCountForCustomerReport(SpecParams spec, int customerID, DateTime firstDate, DateTime secondDate)
        {
            var countReservationSpec = new CountReservationsForCustomerSpec(spec, customerID, firstDate, secondDate);
            return await GetReservationCountAsync(countReservationSpec);
        }

        public async Task<int> GetCountForBookingReport(SpecParams spec, DateTime firstDate, DateTime secondDate)
        {
            var countReservationSpec = new CountReservationsForBookingSpec(spec, firstDate, secondDate);
            return await GetReservationCountAsync(countReservationSpec);
        }

        public async Task<int> GetCountForRevenueReport(SpecParams spec, DateTime firstDate, DateTime secondDate)
        {
            var countReservationSpec = new CountReservationsForRevenueSpec(spec, firstDate, secondDate);
            return await GetReservationCountAsync(countReservationSpec);
        }
        private async Task<int> GetReservationCountAsync(ISpecification<Reservation> spec)
        {
            return await _unitOfWork.Repository<Reservation>().GetCountWithSpecAsync(spec);
        }
    }
}