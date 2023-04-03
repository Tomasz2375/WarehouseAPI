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
        void DeleteOrder(int id);
        string UpdateStatus(int orderId, UpdateStatusDto statusId);
    }

    public class OrderService : IOrderService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(WarehouseDbContext dbContext, IMapper mapper, ILogger<OrderService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            _logger.LogInformation("Invoke: GetOrders()");

            var orders = _dbContext
                .Orders
                .Include(o => o.Status)
                .ToList();
            if (orders is null)
            {
                _logger.LogError($"Error: orders not found");
                throw new NotFoundException("There are no orders");
            }
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return result;
        }
        
        public int AddOrder(AddOrderDto dto)
        {
            _logger.LogInformation("Invoke: AddOrder(dto)");

            var order = _mapper.Map<Order>(dto);
            order.StatusId = 1;
            order.PreparationDate = null;
            order.PostDate = null;
            _dbContext.Add(order);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Added order with id = {order.Id}");

            return order.Id;
        }
        
        public GetOrderDto GetOrderDetails(int id)
        {
            _logger.LogInformation($"Invoke: GetOrderDetails(id = {id})");

            var order = _dbContext
                .Orders
                .Include(o => o.OrderDetails).ThenInclude(od => od.Goods)
                .Include(o => o.Status)
                .FirstOrDefault(o => o.Id == id);

            if (order is null)
            {
                _logger.LogError($"Error: order with id = {id} not found");
                throw new NotFoundException("Order not found");
            }

            _logger.LogInformation($"Get order with id = {id}");
            var result = _mapper.Map<GetOrderDto>(order);
            return result;
        }

        public void DeleteOrder(int id)
        {
            _logger.LogInformation($"Invoke: Delete(orderId = {id})");

            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if(order is null)
            {
                _logger.LogError($"Error: order with id = {id} not found");
                throw new NotFoundException("Order not found");
            }

            _logger.LogInformation($"Deleted order with id = {id}");
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }

        public string UpdateStatus(int orderId, UpdateStatusDto statusId)
        {
            _logger.LogInformation($"Invoke: UpdateStatus(orderId = {orderId}, {statusId.StatusId})");

            var order = _dbContext
                .Orders
                .Include(o => o.Status)
                .FirstOrDefault(o => o.Id == orderId);

            if(order is null)
            {
                _logger.LogError($"Error: order with id = {orderId} not found");
                throw new NotFoundException("Order not found");
            }
            if(order.StatusId == statusId.StatusId)
            {
                return order.Status.Description;
            }
            order.StatusId = statusId.StatusId;

            _dbContext.SaveChanges();

            var statusDescription = _dbContext.Orders.Include(o => o.Status)
                .First(o => o.Id == orderId).Status.Description;
            _logger.LogInformation($"Updated order status with id = {orderId} to: {statusDescription}");

            return statusDescription;
        }
    }
}
