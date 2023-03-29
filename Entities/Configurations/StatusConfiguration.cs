using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WarehouseAPI.Entities.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status() { Id = 1, Description = "Waiting" },
                new Status() { Id = 2, Description = "InPreparation" },
                new Status() { Id = 3, Description = "Prepared" },
                new Status() { Id = 4, Description = "Sent" },
                new Status() { Id = 5, Description = "Rejected" });
        }
    }
}
