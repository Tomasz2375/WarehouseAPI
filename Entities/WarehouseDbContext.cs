using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace WarehouseAPI.Entities
{
    public class WarehouseDbContext : DbContext
    {
        // private string _connectionString =
        // "Server=LAPTOP-V2SU36SG\\SQLEXPRESS01;Database=WarehouseAPI;Trusted_Connection=True;Encrypt=False";
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }   
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

    }
}
