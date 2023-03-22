using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetOrders();
    }

    public class OrderService : IOrderService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderService(WarehouseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<OrderDto> GetOrders()
        {
            var orders = _dbContext
                .Orders
                .Include(o => o.Status)
                .ToList();
            if (orders is null)
            {
                throw new NotFoundException("There are no orders");
            }
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return result;
        }
    }
}
