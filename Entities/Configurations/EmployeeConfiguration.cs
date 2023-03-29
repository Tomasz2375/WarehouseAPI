using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseAPI.Entities.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(25);
            builder.Property(e => e.Surname).IsRequired().HasMaxLength(25);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(25);
            builder.Property(e => e.DateOfBirth).IsRequired();
            builder.HasOne(e => e.Address).WithOne(a => a.Employee);
            builder.HasOne(e => e.Role).WithMany(r => r.Employees).IsRequired();
        }
    }
}
