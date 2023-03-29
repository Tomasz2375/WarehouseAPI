using AutoMapper;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IGoodsService
    {
        GetGoodsDto GetById(int id);
        IEnumerable<GetGoodsDto> GetAll();
        int AddGoogs(AddGoodsDto dto);
        int Update(int id, ModifyGoodsDto dto);
        void Delete(int id);
    }

    public class GoodsService : IGoodsService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        public GoodsService(WarehouseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<GetGoodsDto> GetAll() 
        {
            var goods = _dbContext.Goods;
            var result = _mapper.Map<IEnumerable<GetGoodsDto>>(goods);
            return result;
        }
        public GetGoodsDto GetById(int id)
        {
            var goods = _dbContext
            .Goods
            .FirstOrDefault(g => g.Id == id);

            if (goods is null)
            {
                throw new NotFoundException("Goods not found");
            }

            var result = _mapper.Map<GetGoodsDto>(goods);
            return result;
        }
        public int AddGoogs(AddGoodsDto dto)
        {
            var goods = _mapper.Map<Goods>(dto);
            _dbContext.Goods.Add(goods);
            _dbContext.SaveChanges();
            return goods.Id;
        }
        public int Update(int id, ModifyGoodsDto dto)
        {
            var goods = _dbContext.Goods
                .FirstOrDefault(g => g.Id == id);
            if(goods is null)
            {
                throw new NotFoundException("Goods not found");
            }
            goods.Stock = dto.Stock;
            goods.Price = dto.Price;
            goods.Description = dto.Description;
            _dbContext.SaveChanges();
            return goods.Id;
        }
        public void Delete(int id)
        {
            var goods = _dbContext.Goods.FirstOrDefault(g => g.Id == id);
            if (goods is null)
            {
                throw new NotFoundException("Goods not found");
            }
            _dbContext.Remove(goods);
            _dbContext.SaveChanges();
        }
    }
}
