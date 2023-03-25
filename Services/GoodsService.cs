﻿using AutoMapper;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IGoodsService
    {
        Goods GetById(int id);
        IEnumerable<Goods> GetAll();
        int AddGoogs(AddGoodsDto dto);
        bool Update(int id, ModifyGoodsDto dto);
        bool Delete(int id);
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
        public IEnumerable<Goods> GetAll() 
        {
            var goods = _dbContext.Goods.ToList();
            return goods;
        }
        public Goods GetById(int id)
        {
            var goods = _dbContext
            .Goods
            .FirstOrDefault(g => g.Id == id);

            if (goods == null)
            {
                return null;
            }

            return goods;
        }
        public int AddGoogs(AddGoodsDto dto)
        {
            var goods = _mapper.Map<Goods>(dto);
            _dbContext.Goods.Add(goods);
            _dbContext.SaveChanges();
            return goods.Id;
        }
        public bool Update(int id, ModifyGoodsDto dto)
        {
            var goods = _dbContext.Goods
                .FirstOrDefault(g => g.Id == id);
            if(goods is null)
            {
                return false;
            }
            goods.Stock = dto.Stock;
            goods.Price = dto.Price;
            goods.Description = dto.Description;
            _dbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var goods = _dbContext.Goods.FirstOrDefault(g => g.Id == id);
            if (goods is null) return false;
            _dbContext.Remove(goods);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
