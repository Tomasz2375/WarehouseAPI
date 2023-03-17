using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Controllers
{
    [Route("api/goods")]
    public class GoodsControllers : ControllerBase
    {
        private readonly WarehouseDbContext _dbContext;
        public GoodsControllers(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
