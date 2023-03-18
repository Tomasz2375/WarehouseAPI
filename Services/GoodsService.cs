﻿using AutoMapper;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IGoodsService
    {
        public Goods GetById(int id);
        public IEnumerable<Goods> GetAll();
        public int AddGoogs(AddGoodsDto dto);
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
    }
}