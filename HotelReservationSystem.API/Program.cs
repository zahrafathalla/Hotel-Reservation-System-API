using HotelReservationSystem.API.Errors;
using HotelReservationSystem.API.MiddleWare;
using HotelReservationSystem.Data.Context;
using HotelReservationSystem.Mediator.ReservationMediator;
using HotelReservationSystem.Mediator.RoomMediator;
using HotelReservationSystem.Repository.Interface;
using HotelReservationSystem.Repository.Repository;
using HotelReservationSystem.Service.Services.BackgroundServices;
using HotelReservationSystem.Service.Services.FacilityService;
using HotelReservationSystem.Service.Services.Helper;
using HotelReservationSystem.Service.Services.PaymentService;
using HotelReservationSystem.Service.Services.ReservationService;
using HotelReservationSystem.Service.Services.RoomFacilityService;
using HotelReservationSystem.Service.Services.RoomService;
using HotelReservationSystem.Service.Services.InvoiceService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelReservationSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            #region Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
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
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped< IReservationMediator, ReservationMediator >();
            builder.Services.AddScoped<IInvoiceService,InvoiceService>();


            builder.Services.AddHostedService<ReservationStatusBackgroundService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(E => E.ErrorMessage)
                                                       .ToList();

                    var response = new ApiValidationErrorssResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };



            }); 
            #endregion


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
            

            #region Configure the HTTP request pipeline.[middleWare]
            app.UseMiddleware<ExceptionMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
