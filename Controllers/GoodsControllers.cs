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
        private readonly IGoodsService _service;
        public GoodsControllers(IGoodsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Goods>> GetAllGoods() 
        {
            var goods = _service.GetAll();
            return Ok(goods);
        }
        [HttpGet("{id}")]
        public ActionResult <Goods> GetGoodsFromId([FromRoute] int id)
        {
            var goods = _service.GetById(id);
            if(goods == null) { return NotFound(); }
            return Ok(goods);
        }
        [HttpPost]
        public ActionResult AddGoods([FromBody] AddGoodsDto dto)
        {
            var id = _service.AddGoogs(dto);
            return Created($"/api/goods/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] ModifyGoodsDto dto)
        {
            var isUpdated = _service.Update(id, dto);
            if (!isUpdated) return NotFound();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDelete = _service.Delete(id);
            if(!isDelete) return NoContent();
            return Ok();
        }
    }
}
