using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WarehouseAPI.Entities
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options, ModelBuilder modelBuilder) : base (options)
        {

        }
        public DbSet<Address> Addresses { get; set; }   
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }

    }
}
