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
            CreateMap<ModifyGoodsDto, Goods>();
            CreateMap<Employee, EmployeeDto>()
                .ForMember(e => e.RoleName, c => c.MapFrom(r => r.Role.Name));
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Status, c => c.MapFrom(o => o.Status.Description));
            CreateMap<AddOrderDto, Order>();
            CreateMap<AddOrderDetailsDto, OrderDetails>();
            CreateMap<OrderDetails, GetOrderDetailsDto>()
                .ForMember(od => od.GoodsName, c => c.MapFrom(g => g.Goods.Name));
            CreateMap<Order, GetOrderDto>()
                .ForMember(o => o.Status, c => c.MapFrom(c => c.Status.Description));
        }
    }
}
