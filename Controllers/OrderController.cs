using Microsoft.AspNetCore.Mvc;
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
    }
}
