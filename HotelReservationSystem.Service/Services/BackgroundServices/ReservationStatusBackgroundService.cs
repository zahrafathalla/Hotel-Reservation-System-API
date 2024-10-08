﻿using HotelReservationSystem.Service.Services.ReservationService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservationSystem.Service.Services.BackgroundServices
{
    public class ReservationStatusBackgroundService : BackgroundService 
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReservationStatusBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();

                    await reservationService.UpdateCheckInStatusesAsync();
                    await reservationService.UpdateCheckOutStatusesAsync();

                    await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
