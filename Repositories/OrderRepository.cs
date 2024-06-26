﻿using Repositories;
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

        public int AddNew(Models.Order newOrder)
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
                        OrderId = newOrder.Id,
                        ProductId = productId
                    };

                    context.ItemsForOrders.Add(itemForOrder);
                }
                context.Carts.SingleOrDefault(c => c.Id == newOrder.CartId).IsOpen = false;
                context.Carts.SingleOrDefault(c => c.Id == newOrder.CartId).TotalPrice = 0;
                var products = context.CartProducts.Where(p => p.CartId == newOrder.CartId);
                context.CartProducts.RemoveRange(products);
                
                context.SaveChanges();
                return context.Orders.First(order => order.Id == newOrder.Id).Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool Delete(int orderId)
        {
            if (orderId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Models.Order? order = new Order();
            order = context.Orders.Find(orderId);
            if (order != null)
            {
                var itemsForOrder = context.ItemsForOrders.Where(item => item.OrderId == orderId);
                context.ItemsForOrders.RemoveRange(itemsForOrder);
                context.Orders.Remove(order);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Models.Order> Get(Func<Models.Order, bool>? predicate = null)
        {
            if (predicate == null)
            {
                return context.Orders.ToList();
            }
            return context.Orders.Where(predicate);
        }
        public IEnumerable<Models.Order> GetByUser(int userId)
        {
            return context.Orders.Where(order => order.UserId == userId);
        }

        public Order Get(int orderId)
        {
            return context.Orders.Find(orderId);
        }

        public int GetDeliveryPrice(int cartId)
        {
            int totalDelivery = 0;
            var products = context.Products
                 .Join(
                     context.CartProducts,
                     p => p.Id,
                     cp => cp.ProductId,
                     (p, cp) => new { Product = p, CartId = cp.CartId }
                 )
                 .Where(x => x.CartId == cartId)
                 .Select(x => x.Product)
                 .ToList();
            int productHeavy = products.Where(p => p.Weight == true).ToList().Count();
            int productLight = products.Where(p => p.Weight == false).ToList().Count();
            if (productHeavy > 0)
            {
                totalDelivery = 50 * productHeavy;
                totalDelivery += 15 * productLight;
                return totalDelivery;
            }
            else
            {
                totalDelivery = 15 * productLight + 20;
                return totalDelivery;
            }

        }

        public IEnumerable<Order>? GetByDate(DateOnly date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            IEnumerable<Order> orders = context.Orders.Where(or => or.DateOrder.Year == year && or.DateOrder.Month == month && or.DateOrder.Day == day);
            return orders;
        }
    

        public List<string> GetProductsName(int orderId)
        {
            var productNames = context.Products
                .Where(p => context.ItemsForOrders.Any(it => it.ProductId == p.Id && it.OrderId == orderId))
                .Select(p => p.Name);
            return productNames.ToList();
        }
       
    }
}
