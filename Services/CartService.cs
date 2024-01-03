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
        public DTO.Cart Get(int id)
        {
            if (id < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            DTO.Cart cart;
            cart = mapper.Map<DTO.Cart>(repository.Get(id));
            return cart;
        }
        public DTO.Cart GetByUser(int id)
        {
            if (id < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            DTO.Cart cart;
            cart = mapper.Map<DTO.Cart>(repository.GetByUser(id));
            return cart;
        }
        public List<int> ProductIsAvialible(int productId,string productType, DateTime from, DateTime to)
        {
            
            return repository.ProductIsAvialible(productId, productType, from, to); 
        }
        //public int AddToCart(int userId, int productId)
        //{

        //    return repository.AddToCart(userId,productId);
        //}
        public int AddToCart(int userId, int productId, DateTime from, DateTime to)
        {
            return repository.AddToCart(userId, productId, from, to);
        }

        public bool UpdateDate(int cartId, DateTime from, DateTime to)
        {
            return repository.UpdateDate(cartId, from, to);
        }
        public bool Delete(int id)
        {
            
            return repository.Delete(id);
        }
        public double GetTotalPrice(int cartId)
        {
           return repository.GetTotalPrice(cartId);
        }
        
    }
}
