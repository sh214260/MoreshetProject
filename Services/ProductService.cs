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
using Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services
{
    public class ProductService:IProductService
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
                return repository.AddNew(mapper.Map<Repositories.Models.Product>(newProduct));
                 
            }
            return false;
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
        public List<DTO.Product> GetAvailable(DateTime from, DateTime to)        {
            List<Repositories.Models.Product> ModelsProducts = repository.GetAvailable(from, to);
            List<DTO.Product> products = ModelsProducts.Select(pr => mapper.Map<Repositories.Models.Product, DTO.Product>(pr)).ToList();
            return products;
        }
        public bool Delete(int productId)
        {
           
                if (productId < 0)
                {
                    throw new EntityNotFoundExceptions();
                }
              return  repository.Delete(productId);


        }

        public IEnumerable<string> GetImages()
        {
            return repository.GetImages();
        }

        public bool Update(DTO.Product product)
        {
            if (product==null)
            {
                throw new EntityNotFoundExceptions();
            }
            Repositories.Models.Product modproduct = mapper.Map<Repositories.Models.Product>(product);
            return repository.Update(modproduct);
        }
    }
}
