using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using AutoMapper;
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

        public IEnumerable<DTO.Product> Get(Func<DTO.Product, bool>? predicate = null)
        {
            IEnumerable <Repositories.Models.Product> ModelsProducts= repository.Get();
            IEnumerable<DTO.Product> products = ModelsProducts.Select(pr => mapper.Map<Repositories.Models.Product, DTO.Product>(pr));
            return products;
        }
    }
}
