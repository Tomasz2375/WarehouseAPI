using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/order")]
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
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var id = _orderService.AddOrder(dto);
            return Created($"api/order/{id}", null);
        }
    }
}
