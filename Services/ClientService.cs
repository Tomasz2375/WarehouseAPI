using AutoMapper;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IClientService
    {
        int AddClient(AddClientDto dto);
        IEnumerable<GetClientDto> GetClients();
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
        public int AddClient(AddClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            _dbContext.Add(client);
            _dbContext.SaveChanges();
            return client.Id;
        }
        public IEnumerable<GetClientDto> GetClients()
        {
            var clients = _dbContext.Clients;
            var result = _mapper.Map<IEnumerable<GetClientDto>>(clients);
            return result;
        }
    }
}
