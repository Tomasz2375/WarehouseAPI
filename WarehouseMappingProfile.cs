using AutoMapper;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<AddGoodsDto, Goods>();
        }
    }
}
