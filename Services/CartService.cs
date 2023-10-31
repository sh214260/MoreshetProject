using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
namespace Services
{
    public class CartService:ICartService
    {
        private readonly IMapper mapper;
        private readonly ICartRepository repository;
        public CartService(ICartRepository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }
        public bool ProductIsAvialible(string type, DateTime from, DateTime to)
        {
            
            return repository.ProductIsAvialible(type, from, to); 
        }
        public DTO.Cart AddToCart(int userId, int productId)
        {
            Repositories.Models.Cart _cart = repository.AddToCart(userId, productId);
            DTO.Cart cart = mapper.Map<Repositories.Models.Cart, DTO.Cart>(_cart);
            return cart;
        }
    }
}
