using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Interfaces;
namespace Services
{
    public class ItemForOrderService : IItemForOrderService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.IItemForOrderRepository repository;
        public ItemForOrderService(IItemForOrderRepository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }

        public bool Delete(int orderId, int productId)
        {
            return repository.Delete(orderId, productId);
        }

        public IEnumerable<DTO.Product> GetProducts(int orderId)
        {
            IEnumerable<Repositories.Models.Product> ModelsProducts = repository.GetProducts(orderId);
            IEnumerable<DTO.Product> products = ModelsProducts.Select(pr => mapper.Map<Repositories.Models.Product, DTO.Product>(pr));
            return products;
        }
    }
}
