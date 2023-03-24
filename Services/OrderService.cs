using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IOrderService
    {
        int AddOrder(AddOrderDto dto);
        IEnumerable<OrderDto> GetOrders();
        GetOrderDto GetOrderDetails(int id);
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
        public int AddOrder(AddOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.StatusId = 1;
            order.PreparationDate = null;
            order.PostDate = null;
            _dbContext.Add(order);
            _dbContext.SaveChanges();
            return order.Id;
        }
        public GetOrderDto GetOrderDetails(int id)
        {
            var order = _dbContext
                .Orders
                .Include(o => o.OrderDetails).ThenInclude(od => od.Goods)
                .Include(o => o.Status)
                .FirstOrDefault(o => o.Id == id);

            if (order is null) throw new NotFoundException("Order not found");

            var result = _mapper.Map<GetOrderDto>(order);
            return result;
        }
    }
}
