using Microsoft.EntityFrameworkCore;
using Presentation2.Data.Entities;

namespace Presentation2.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BookingEntity>? Bookings { get; set; }

    public DbSet<BookingOwnerEntity>? BookingOwners { get; set; }
    public DbSet<BookingAddressEntity>? BookingAddresses { get; set; }
}
