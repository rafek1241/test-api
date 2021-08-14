using Api.Test.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Test.Infrastructure
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .OwnsOne(p => p.Address);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}