using DataAnnotation.Models;
using Microsoft.EntityFrameworkCore;
namespace DataAnnotation.Data
{
    public class DataAnnotationContext : DbContext
    {
        public DataAnnotationContext(DbContextOptions<DataAnnotationContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
    }
}
