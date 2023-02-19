using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Models;

namespace HotelBookingAPI.Data
{
    public class ApiContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().OwnsOne(x => x.Request);
        }

        public DbSet<Passenger> Passengers { get; set; } //TODO: add MySQL not internal db
       
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {
           
        }

    }
}
