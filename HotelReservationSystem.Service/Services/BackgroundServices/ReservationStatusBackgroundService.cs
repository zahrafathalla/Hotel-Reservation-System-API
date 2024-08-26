using HotelReservationSystem.Service.Services.ReservationService;
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
                using var scope = _serviceScopeFactory.CreateScope();
                var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
                await reservationService.UpdateCheckInStatusesAsync();
                await reservationService.UpdateCheckOutStatusesAsync();

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); 
            }
        }
    }
}
