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
        public int Update(int orderId, int orderDetailsId, int quantity)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if(order is null)
            {
                throw new NotFoundException("Order not found");
            }
            var orderDetails = _dbContext.OrderDetails.FirstOrDefault(o => o.Id == orderDetailsId);
            if (orderDetails is null || orderDetails.OrderId != orderId)
            {
                throw new NotFoundException("This order deteils does not exist in currant context");
            }
            orderDetails.Quantity = quantity;
            _dbContext.SaveChanges();
            return orderDetails.Id;              
        }
    }
}
