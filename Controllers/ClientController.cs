using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("/api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        public ActionResult AddClient([FromBody] AddClientDto dto) 
        {
            var clientId = _clientService.AddClient(dto);
            return Created($"/api/client/{clientId}", null);
        }

    }
}
