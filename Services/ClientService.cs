﻿using AutoMapper;
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
        private ILogger<ClientService> _logger;

        public ClientService(WarehouseDbContext dbContext, IMapper mapper, ILogger<ClientService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public int AddClient(ClientDto dto)
        {
            _logger.LogInformation("Invoke: AddClient(dto)");

            var client = _mapper.Map<Client>(dto);
            _dbContext.Add(client);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Added client with Id = {client.Id}");

            return client.Id;
        }

        public IEnumerable<GetClientsDto> GetClients()
        {
            _logger.LogInformation("Invoke: GetClients()");

            var clients = _dbContext.Clients;
            var result = _mapper.Map<IEnumerable<GetClientsDto>>(clients);
            return result;
        }

        public ClientDto GetClientById(int clientId) 
        {
            _logger.LogInformation($"Invoke: GetClientsById(id = {clientId}");

            var client = _dbContext.Clients
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                _logger.LogError($"Error: client with id = {clientId} not found");
                throw new NotFoundException("Client not found");
            }

            _logger.LogInformation($"Get client with id = {clientId}");
            var result = _mapper.Map<ClientDto>(client);
            return result;
        }

        public int Update(int clientId, ClientDto dto)
        {
            _logger.LogInformation($"Invoke: Update(clientId = {clientId}, dto)");

            var client = _dbContext.Clients
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == clientId);

            if(client == null)
            {
                _logger.LogError($"Error: client with id = {clientId} not found");
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

            _logger.LogInformation($"Updated client details with id = {clientId}");

            _dbContext.SaveChanges();

            return client.Id;
        }

        public void Delete(int clientId)
        {
            _logger.LogInformation($"Invoke: Delete(clientId = {clientId})");

            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                _logger.LogError($"Error: client with id = {clientId} not found");
                throw new NotFoundException("Client not found");
            }

            _logger.LogInformation($"Deleted client with id = {clientId}");
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }
    }
}
