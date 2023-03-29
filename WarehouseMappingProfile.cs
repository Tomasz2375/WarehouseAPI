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
                .ForMember(e => e.RoleName, imapper => imapper.MapFrom(r => r.Role.Name));
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Status, imapper => imapper.MapFrom(o => o.Status.Description));
            CreateMap<AddOrderDto, Order>();
            CreateMap<AddOrderDetailsDto, OrderDetails>();
            CreateMap<OrderDetails, GetOrderDetailsDto>()
                .ForMember(od => od.GoodsName, imapper => imapper.MapFrom(g => g.Goods.Name));
            CreateMap<Order, GetOrderDto>()
                .ForMember(o => o.Status, imapper => imapper.MapFrom(c => c.Status.Description));
            CreateMap<ClientDto, Client>()
                .ForPath(c => c.Address.Country, imapper => imapper.MapFrom(a => a.Country))
                .ForPath(c => c.Address.City, imapper => imapper.MapFrom(a => a.City))
                .ForPath(c => c.Address.Street, imapper => imapper.MapFrom(a => a.Street))
                .ForPath(c => c.Address.PostalCode, imapper => imapper.MapFrom(a => a.PostalCode))
                .ForPath(c => c.Address.HouseNumber, imapper => imapper.MapFrom(a => a.HouseNumber));
            CreateMap<Client, ClientDto>()
                .ForMember(a => a.Country, imapper => imapper.MapFrom(c => c.Address.Country))
                .ForMember(a => a.City, imapper => imapper.MapFrom(c => c.Address.City))
                .ForMember(a => a.Street, imapper => imapper.MapFrom(c => c.Address.Street))
                .ForMember(a => a.PostalCode, imapper => imapper.MapFrom(c => c.Address.PostalCode))
                .ForMember(a => a.HouseNumber, imapper => imapper.MapFrom(c => c.Address.HouseNumber));
            CreateMap<Client, GetClientsDto>();
            CreateMap<Goods, GetGoodsDto>();
        }
    }
}
