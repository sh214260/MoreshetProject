using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CartProductService:ICartProductService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.ICartProductRepository repository;
        public CartProductService(ICartProductRepository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }
        public IEnumerable<DTO.CartProduct> Get(int cartId)
        {
            IEnumerable<Repositories.Models.CartProduct> ModelsCartProduct = repository.Get(cartId);
            IEnumerable<DTO.CartProduct> products = ModelsCartProduct.Select(pr => mapper.Map<Repositories.Models.CartProduct, DTO.CartProduct>(pr));
            return products;
        }
        public IEnumerable<DTO.Product> GetProducts(int cartId)
        {
            IEnumerable<Repositories.Models.Product> ModelsProducts = repository.GetProducts(cartId);
            IEnumerable<DTO.Product> products = ModelsProducts.Select(pr => mapper.Map<Repositories.Models.Product, DTO.Product>(pr));
            return products;
        }

    }
}
