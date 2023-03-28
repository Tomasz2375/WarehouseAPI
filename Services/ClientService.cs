using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IClientService
    {
        int AddClient(ClientDto dto);
        IEnumerable<GetClientsDto> GetClients();
        ClientDto GetClientById(int clientId);
        int Update(int clientId, ClientDto dto);
        void Delete(int clientId);
    }

    public class ClientService : IClientService
    {
        private WarehouseDbContext _dbContext;
        private IMapper _mapper;
        public ClientService(WarehouseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int AddClient(ClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            _dbContext.Add(client);
            _dbContext.SaveChanges();
            return client.Id;
        }
        public IEnumerable<GetClientsDto> GetClients()
        {
            var clients = _dbContext.Clients;
            var result = _mapper.Map<IEnumerable<GetClientsDto>>(clients);
            return result;
        }
        public ClientDto GetClientById(int clientId) 
        {
            var client = _dbContext.Clients
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            var result = _mapper.Map<ClientDto>(client);
            return result;
        }
        public int Update(int clientId, ClientDto dto)
        {
            var client = _dbContext.Clients
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == clientId);
            if(client == null)
            {
                throw new NotFoundException("Client not found");
            }
            
            client.Name = dto.Name;
            client.Email = dto.Email;
            client.PhoneNumber = dto.PhoneNumber;
            client.Address.Country = dto.Country;
            client.Address.City = dto.City;
            client.Address.Street = dto.Street;
            client.Address.PostalCode = dto.PostalCode;
            client.Address.HouseNumber = dto.HouseNumber;

            _dbContext.SaveChanges();

            return client.Id;
        }
        public void Delete(int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }
    }
}
