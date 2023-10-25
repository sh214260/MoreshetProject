using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
namespace Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Repositories.Models.User,DTO.User>().ReverseMap();
            CreateMap<Repositories.Models.Category,DTO.Category>().ReverseMap();
            CreateMap<Repositories.Models.Product, DTO.Product>().ReverseMap();
            CreateMap<Repositories.Models.Order, DTO.Order>().ReverseMap();
            CreateMap<Repositories.Models.ItemsForOrder, DTO.ItemsForOrder>().ReverseMap();
            CreateMap<Repositories.Models.Cart, DTO.Cart>().ReverseMap();
            CreateMap<Repositories.Models.CartProduct, DTO.CartProduct>().ReverseMap();
        }

    }
}
