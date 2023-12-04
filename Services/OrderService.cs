using AutoMapper;
using DTO;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService:Interfaces.IOrderService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.IOrderRepository repository;
        public OrderService(IOrderRepository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }
        public int AddNew(DTO.Order newOrder)
        {
            if (newOrder != null)
            {
               return repository.AddNew(mapper.Map<Repositories.Models.Order>(newOrder));
                 
            }
            return -1;
        }

        public void Delete(int orderId)
        {
            if (orderId < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            try
            {
                repository.Delete(orderId);
            }
            catch
            {

            }
        }

        public DTO.Order Get(int orderId)
        {
            if (orderId < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            try
            {
                Repositories.Models.Order user1 = repository.Get(orderId);
                DTO.Order order;
                order = mapper.Map<DTO.Order>(user1);
                return order;
            }
            catch
            {
                //to do: ex 
                throw;
            }
        }
        public IEnumerable<DTO.Order> Get(Func<Repositories.Models.Order, bool>? predicate = null)
        {
            
            IEnumerable<Repositories.Models.Order> ModelsOrder = repository.Get(predicate);
            if (ModelsOrder == null)
            {
                return null;
            }
            IEnumerable<DTO.Order> orders = ModelsOrder.Select(pr => mapper.Map<Repositories.Models.Order, DTO.Order>(pr));
            if(orders== null)
            {
                throw new EmptyListException();
            }
            return orders;
            
        }

        
    }
}
