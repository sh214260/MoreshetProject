using Repositories;
using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public OrderRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public bool AddNew(Models.Order newOrder)
        {
            try
            {
                context.Orders.Add(newOrder);
                context.SaveChanges();
                var productsId = (from cp in context.CartProducts
                                  join c in context.Carts on cp.CartId equals c.Id
                                  where c.Id == newOrder.CartId
                                  select cp.ProductId).ToList();
                foreach (var productId in productsId)
                {
                    Models.ItemsForOrder itemForOrder = new Models.ItemsForOrder()
                    {
                        //Id = 0,
                        OrderId = newOrder.Id,
                        ProductId =productId
                    };

                    context.ItemsForOrders.Add(itemForOrder);
                }
                context.Carts.SingleOrDefault(c => c.Id == newOrder.CartId).IsOpen=false;
                
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int orderId)
        {
            try
            {
                if (orderId < 0)
                {
                    //to do: ex
                    throw new ArgumentOutOfRangeException();
                }
                Models.Order order = new Order();
                order = context.Orders.Find(orderId);
                context.Orders.Remove(order);
                context.SaveChanges();
            }
            catch
            {

            }
        }

        public IEnumerable<Models.Order> Get(Func<Models.Order, bool>? predicate = null)
        {
            if (predicate == null)
            {
                return context.Orders.ToList();
            }
            return context.Orders.Where(predicate);
        }

        public Order Get(int orderId)
        {
            return context.Orders.Find(orderId);
        }
    }
}
