using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IOrderDetailsService
    {
        int Create(int orderId, AddOrderDetailsDto dto);
        int Update(int orderId, int orderDetailsId, int quantity);
        void Delete(int orderId, int orderDetailsId);
    }

    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderDetailsService> _logger;

        public OrderDetailsService(WarehouseDbContext dbContext, IMapper mapper, ILogger<OrderDetailsService> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        public int Create(int orderId, AddOrderDetailsDto dto)
        {
            _logger.LogInformation($"Invoke: Create(orderId = {orderId}, AddOrderDetailsDto)");

            var order = _dbContext.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == orderId);

            if (order is null)
            {
                _logger.LogError($"Error: order with id = {orderId} not found");
                throw new NotFoundException("Order not found");
            }
            var addOrderDetails = _mapper.Map<OrderDetails>(dto);
            addOrderDetails.OrderId = orderId;


            if (order.OrderDetails.Any(od => od.GoodsId == dto.GoodsId))
            {
                var orderDetailsId = order.OrderDetails.First(od => od.GoodsId == dto.GoodsId).Id;
                throw new AlreadyExistExceptions($"This goods was already added. Try use method Update or Delete on orderDetailsId = {orderDetailsId}.");
            }
            _dbContext.Add(addOrderDetails);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Added order details with id = {addOrderDetails.Id} to order with Id = {order.Id}");

            return addOrderDetails.Id;
        }

        public int Update(int orderId, int orderDetailsId, int quantity)
        {
            _logger.LogInformation($"Invoke: Update(orderId = {orderId}, orderDetailsId = {orderDetailsId}, quantity = {quantity})");
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if(order is null)
            {
                _logger.LogError($"Error: order with id = {orderId} not found");
                throw new NotFoundException("Order not found");
            }
            var orderDetails = _dbContext.OrderDetails.FirstOrDefault(o => o.Id == orderDetailsId);
            if (orderDetails is null || orderDetails.OrderId != orderId)
            {
                _logger.LogError($"Error: order details with id = {orderDetailsId} does not exist in currant context");
                throw new NotFoundException("This order deteils does not exist in currant context");
            }
            orderDetails.Quantity = quantity;
            _dbContext.SaveChanges();

            _logger.LogInformation($"Updated goods quantity in order details with id = {orderDetailsId} to {quantity}");

            return orderDetails.Id;              
        }
        
        public void Delete(int orderId, int orderDetailsId) 
        {
            _logger.LogInformation($"Invoke: Delete(orderId = {orderId}, orderDetailId = {orderDetailsId})");

            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                _logger.LogError($"Error: order with id = {orderId} not found");
                throw new NotFoundException("Order not found");
            }
            var orderDetails = _dbContext.OrderDetails.FirstOrDefault(od => od.Id == orderDetailsId);
            if(orderDetails is null || orderDetails.OrderId != orderId)
            {
                _logger.LogError($"Error: order details with id = {orderDetailsId} does not exist in currant context");
                throw new NotFoundException("This order deteils does not exist in currant context");
            }
            _dbContext.Remove(orderDetails);
            _logger.LogInformation($"Deleted order details with id = {orderDetailsId}");
            _dbContext.SaveChanges();
        }
    }
}
