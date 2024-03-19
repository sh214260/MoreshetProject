using AutoMapper;
using DTO;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Interfaces;
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
        private readonly Services.Interfaces.IUserService UserService;
        private readonly Services.Interfaces.IItemForOrderService ItemsService;

        public OrderService(IOrderRepository dal, IMapper _mapper, IUserService userService, IItemForOrderService itemsService)
        {
            mapper = _mapper;
            repository = dal;
            UserService = userService;
            ItemsService = itemsService;
        }
        public int AddNew(DTO.Order newOrder)
        {
            if (newOrder != null)
            {

               return repository.AddNew(mapper.Map<Repositories.Models.Order>(newOrder));
                 
            }
            return -1;
        }

        public bool Delete(int orderId)
        {
            if (orderId < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            return repository.Delete(orderId);
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

        public IEnumerable<DTO.OrderByDay> GetByDate(DateOnly date)
        {
            IEnumerable<Repositories.Models.Order> orders = repository.GetByDate(date).ToList();
            List<DTO.OrderByDay> ordersDay = new List<DTO.OrderByDay>();

            foreach (var order in orders)
            {
                List<string> names = repository.GetProductsName(order.Id);
                string? userName = UserService.Get(order.UserId).Name;
                DTO.OrderByDay orderDto = new DTO.OrderByDay
                {
                    OrderId = order.Id,
                    FromDate = order.FromDate,
                    ToDate = order.ToDate,
                    ProductsName = names,
                    UserName = userName
                };
                ordersDay.Add(orderDto);
            }
            return ordersDay;
        }


        public IEnumerable<DTO.Order> GetByUser(int userId)
        {
            IEnumerable<Repositories.Models.Order> ModelsOrder = repository.GetByUser(userId);
            if (ModelsOrder == null)
            {
                return null;
            }
            IEnumerable<DTO.Order> orders = ModelsOrder.Select(pr => mapper.Map<Repositories.Models.Order, DTO.Order>(pr));
            if (orders == null)
            {
                return null;
            }
            return orders;
        }

        public int GetDeliveryPrice(int cartId)
        {
           return repository.GetDeliveryPrice(cartId);
        }
        public OrderData GetAllData(int orderId)
        {
            DTO.OrderData orderData = new DTO.OrderData();
            orderData.order = Get(orderId);
            orderData.user = UserService.Get(orderData.order.UserId);
            orderData.products = ItemsService.GetProducts(orderId);
            return orderData;
        }

       
    }
}
