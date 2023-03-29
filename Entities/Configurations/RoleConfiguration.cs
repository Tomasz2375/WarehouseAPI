using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseAPI.Entities.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = 1, Name = "OrderPicker" },
                new Role() { Id = 2, Name = "Warehouseman" },
                new Role() { Id = 3, Name = "Planner" },
                new Role() { Id = 4, Name = "Administrator" });
        }
    }
}
