
using HotelReservationSystem.Data.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Data.Context
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Facility> facilities { get; set; }
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<Staff> staff { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<FeedBack> feedBacks { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Offer> offers { get; set; }
        public DbSet<OfferRoom> OfferRoms { get; set; }

    }
}
