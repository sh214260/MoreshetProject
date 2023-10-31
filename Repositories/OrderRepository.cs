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
