using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseAPI.Entities.Configurations
{
    public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.Property(g => g.Name).IsRequired().HasMaxLength(25);
            builder.Property(g => g.Price).HasPrecision(14, 2);
            builder.HasMany(g => g.OrderDetails).WithOne(od => od.Goods);
        }
    }
}
