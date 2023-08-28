using AdvancedDatabaseandORMConcepts.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedDatabaseandORMConcepts.Database
{
    public class ECommerceDBContext : DbContext
    {
        public ECommerceDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!; 
        public DbSet<CustomerAddress> CustomersAddresses { get; set; } = null!;
        public DbSet<Address> Address { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<Order> Order { get; set; } = null!;
    }
}
