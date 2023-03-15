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
            modelBuilder.Entity<Employee>(etb =>
            {
                etb.Property(e => e.Name).IsRequired().HasMaxLength(25);
                etb.Property(e => e.Surname).IsRequired().HasMaxLength(25);
                etb.Property(e => e.Email).IsRequired().HasMaxLength(25);
                etb.Property(e => e.DateOfBirth).IsRequired().HasPrecision(3);
                etb.HasOne(e => e.Address).WithOne(a => a.Employee);
                etb.HasOne(e => e.Role).WithMany(r => r.Employees);
            });
            modelBuilder.Entity<Goods>(etb =>
            {
                etb.Property(g => g.Name).IsRequired().HasMaxLength(25);
                etb.Property(g => g.Price).HasPrecision(14, 2);
                etb.HasMany(g => g.OrderDetails).WithOne(od => od.Goods);
            });
            modelBuilder.Entity<Order>(etb =>
            {
                etb.Property(o => o.AdmissionDate).HasPrecision(3);
                etb.Property(o => o.PreparationDate).HasPrecision(3);
                etb.Property(o => o.PostDate).HasPrecision(3);
                etb.Property(o => o.RequireDate).IsRequired().HasPrecision(3);
                etb.HasOne(o => o.Status).WithOne(s => s.Order);
                etb.HasMany(o => o.OrderDetails).WithOne(od => od.Order);
            });
            modelBuilder.Entity<OrderDetails>(etb =>
            {
                etb.Property(od => od.Quantity).IsRequired();
                etb.Property(od => od.Price).HasPrecision(14, 2);
                etb.Property(od => od.TotalPrice).HasPrecision(14, 2);
            });
            modelBuilder.Entity<Role>(etb =>
            {
                etb.HasData(
                    new Role() { Id = 1, Name = "OrderPicker" },
                    new Role() { Id = 2, Name = "Warehouseman" },
                    new Role() { Id = 3, Name = "Planner" },
                    new Role() { Id = 4, Name = "Administrator" } );
            });


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

    }
}
