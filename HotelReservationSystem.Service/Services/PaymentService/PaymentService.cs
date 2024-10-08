﻿using AutoMapper;
using HotelReservationSystem.Data.Entities;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Specification.ReservationSpecifications;
using HotelReservationSystem.Service.Services.PaymentService.Dtos;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace HotelReservationSystem.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(
            IConfiguration configuration,
            IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReservationForPaymentToReturnDto> CreatePaymentIntentAsync(int reservationId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(reservationId);
            if (reservation == null)
                return null;

            var totalAmount = reservation.TotalAmount;

            PaymentIntent paymentIntent;
            var paymentIntentService = new PaymentIntentService();

            if (string.IsNullOrEmpty(reservation.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(totalAmount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                reservation.PaymentIntentId = paymentIntent.Id;
                reservation.ClientSecret = paymentIntent.ClientSecret;
            }

            _unitOfWork.Repository<Reservation>().Update(reservation);
            await _unitOfWork.SaveChangesAsync();

            var mappedReservation = _mapper.Map<ReservationForPaymentToReturnDto>(reservation);
            return mappedReservation;
        }

        public async Task<bool> ConfirmPaymentAsync(string paymentIntentId , string paymentMethodId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var service = new PaymentIntentService();
            var options = new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethodId
            };

            PaymentIntent paymentIntent = await service.ConfirmAsync(paymentIntentId, options);

            if (paymentIntent.Status != "succeeded" || paymentIntent == null)
                return false;

            return true;
        }


        public async Task<Reservation> UpdateReservationStatusAsync(string paymnetIntentId, bool isPaid)
        {
            var reservationRepo = _unitOfWork.Repository<Reservation>();
            var spec = new ReservationSpecificationWithPaymentIntent(paymnetIntentId);

            var reservation = await reservationRepo.GetByIdWithSpecAsync(spec);

            if (reservation == null)
                return null;

            if (isPaid)
            {
                if (reservation.Status == ReservationStatus.Pending)
                {
                    reservation.Status = ReservationStatus.PaymentReceived;
                }
            }
            else
            {
                if (reservation.Status != ReservationStatus.CheckedOut && reservation.Status != ReservationStatus.Cancelled)
                {
                    reservation.Status = ReservationStatus.Cancelled;
                }
            }

            reservationRepo.Update(reservation);
            await _unitOfWork.SaveChangesAsync();

            return reservation;

        }

    }
}
