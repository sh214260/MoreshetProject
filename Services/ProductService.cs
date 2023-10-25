using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using AutoMapper;
using Repositories.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DTO;


namespace Services
{
    public class ProductService: Interfaces.IProductService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.IProductRpository repository;
        public ProductService(IProductRpository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }

        public bool AddNew(DTO.Product newProduct)
        {
            if (newProduct != null)
            {
                repository.AddNew(mapper.Map<Repositories.Models.Product>(newProduct));
                return true;
            }
            return false;
        }

        public DTO.CartProduct AddToCart(int userId, int productId)
        {
            Repositories.Models.CartProduct cartProduct = repository.AddToCart(userId, productId);
            DTO.CartProduct cart = mapper.Map<Repositories.Models.CartProduct,DTO.CartProduct>(cartProduct);
            return cart;
        }

        public void Delete(int productId)
        {
            try
            {
                if (productId < 0)
                {
                    throw new EntityNotFoundExceptions();
                }
                repository.Delete(productId);
                
            }
            catch
            {
                
            }
        }

        public DTO.Product Get(int id)
        {
            if (id < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            DTO.Product product;
            product = mapper.Map<DTO.Product>(repository.Get(id));
            return product;
        }

        public IEnumerable<DTO.Product> Get(Func<Repositories.Models.Product, bool>? predicate = null)
        {
            IEnumerable <Repositories.Models.Product> ModelsProducts= repository.Get(predicate);
            IEnumerable<DTO.Product> products = ModelsProducts.Select(pr => mapper.Map<Repositories.Models.Product, DTO.Product>(pr));
            return products;
        }

        
    }
}
