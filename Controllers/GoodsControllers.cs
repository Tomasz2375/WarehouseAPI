using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/goods")]
    [ApiController]
    public class GoodsControllers : ControllerBase
    {
        private readonly IGoodsService _goodsService;
        public GoodsControllers(IGoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Goods>> GetAllGoods() 
        {
            var goods = _goodsService.GetAll();
            return Ok(goods);
        }
        [HttpGet("{id}")]
        public ActionResult <Goods> GetGoodsFromId([FromRoute] int id)
        {
            var goods = _goodsService.GetById(id);
            return Ok(goods);
        }
        [HttpPost]
        public ActionResult AddGoods([FromBody] AddGoodsDto dto)
        {
            var id = _goodsService.AddGoogs(dto);
            return Created($"/api/goods/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] ModifyGoodsDto dto)
        {
            var goodsId = _goodsService.Update(id, dto);
            return Ok($"Updated goods with Id {goodsId}.");
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _goodsService.Delete(id);
            return Ok();
        }
    }
}
