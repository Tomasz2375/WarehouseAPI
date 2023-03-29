using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseAPI.Entities.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.AdmissionDate).IsRequired();
            builder.Property(o => o.RequireDate).IsRequired();
            builder.HasOne(o => o.Status).WithMany(s => s.Orders);
            builder.HasMany(o => o.OrderDetails).WithOne(od => od.Order);
        }
    }
}
