using WarehouseAPI.Entities;

namespace WarehouseAPI
{
    public class WarehouseSeeder
    {
        private readonly WarehouseDbContext _dbContext;
        public WarehouseSeeder(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
