﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/goods")]
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _service.AddGoogs(dto);
            return Created($"/api/goods/{id}", null);
        }
    }
}