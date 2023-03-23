using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IOrderDetailsService
    {
        int Create(int orderId, AddOrderDetailsDto dto);
    }

    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderDetailsService(WarehouseDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public int Create(int orderId, AddOrderDetailsDto dto)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            var addOrder = _mapper.Map<OrderDetails>(dto);
            addOrder.OrderId = orderId;

            _dbContext.Add(addOrder);
            _dbContext.SaveChanges();
            return addOrder.Id;
        }
    }
}
