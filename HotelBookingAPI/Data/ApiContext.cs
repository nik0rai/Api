using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Models;

namespace HotelBookingAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Passenger> Passengers { get; set; } //TODO: add MySQL not internal db
       
        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }

    }
}
