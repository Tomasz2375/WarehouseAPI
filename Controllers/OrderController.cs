using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public ActionResult <IEnumerable<OrderDto>> GetAll()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);

        }
        [HttpPost]
        public ActionResult AddOrder([FromBody] AddOrderDto dto)
        {
            var id = _orderService.AddOrder(dto);
            return Created($"api/order/{id}", null);
        }
        [HttpGet("{orderId}")]
        public ActionResult GetOrderDetails([FromRoute] int orderId)
        {
            var orderDetails = _orderService.GetOrderDetails(orderId);
            return Ok(orderDetails);
        }
    }
}
