using HotelReservationSystem.Data.Context;
using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Repository;
using HotelReservationSystem.Service.Services.FacilityService;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.RoomFacilityService;
using HotelReservationSystem.Service.Services.RoomService;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelReservationSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

          

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging(); ;
            });

            builder.Services.AddScoped<IunitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IFacilityService, FacilityService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IRoomFacilityService, RoomFacilityService>();            
            builder.Services.AddScoped<IRoomFacilityService, RoomFacilityService>();
            builder.Services.AddScoped<IRoomMediator, RoomMediator>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();
            #region Apply All Pending Migrations[Update-Database] and Data Seeding
            using var scoped = app.Services.CreateScope();
            var services = scoped.ServiceProvider;
            var _dbcontext = services.GetRequiredService<ApplicationDBContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                _dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
