using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Controllers
{
    [Route("api/goods")]
    public class GoodsControllers : ControllerBase
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        public GoodsControllers(WarehouseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Goods>> GetAllGoods() 
        {
            var goods = _dbContext.Goods.ToList();
            return Ok(goods);
        }
        [HttpGet("id")]
        public ActionResult <Goods> GetGoodsFromId([FromRoute] int id)
        {
            var goods = _dbContext
                .Goods
                .FirstOrDefault(g => g.Id == id);

            if(goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }
        [HttpPost]
        public ActionResult AddGoods([FromBody] AddGoodsDto dto)
        {
            var goods = _mapper.Map<Goods>(dto);
            _dbContext.Goods.Add(goods);
            _dbContext.SaveChanges();
            return Created($"/api/goods/{goods.Id}", null);
        }
    }
}
