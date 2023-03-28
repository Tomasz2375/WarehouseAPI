using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;
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
        public ActionResult AddClient([FromBody] ClientDto dto) 
        {
            var clientId = _clientService.AddClient(dto);
            return Created($"/api/client/{clientId}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<GetClientsDto>> GetAll()
        {
            var clients = _clientService.GetClients();
            return Ok(clients);
        }
        [HttpGet("{clientId}")]
        public ActionResult <ClientDto> GetClientById([FromRoute] int clientId)
        {
            var client = _clientService.GetClientById(clientId);
            return Ok(client);
        }
        [HttpPut("{clientId}")]
        public ActionResult Update([FromRoute] int clientId, [FromBody] ClientDto dto) 
        {
            var result = _clientService.Update(clientId, dto);
            return Ok($"Updated client data with Id {clientId}.");
        }
        [HttpDelete("{clientId}")]
        public ActionResult Delete([FromRoute] int clientId)
        {
            _clientService.Delete(clientId);
            return NoContent();
        }
    }
}
