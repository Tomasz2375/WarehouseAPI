using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/order/{orderId}/orderDetails")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;
        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }
        [HttpPost]
        public ActionResult AddOrderDetails
            ([FromRoute] int orderId, [FromBody] AddOrderDetailsDto dto)
        {
            var id = _orderDetailsService.Create(orderId, dto);
            return Created($"api/order/{orderId}/orderDetails/{id}", null);
        }
        [HttpPut("{orderDetailsId}")]
        public ActionResult UpdateOrderDetails
            ([FromRoute] int orderId, [FromRoute] int orderDetailsId, [FromBody] int quantity)
        {
            var id = _orderDetailsService.Update(orderId, orderDetailsId, quantity);
            return Ok($"api/order/{orderId}/orderDetails/{id}");
        }

    }
}
