using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public CartRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }
        public Cart Get(int id)
        {
            if (id < 0)
            {
                //to do: ex
                throw new ArgumentOutOfRangeException();
            }
            Models.Cart cart = new Cart();
            cart = context.Carts.Find(id);
            return cart;

        }
        public int ProductIsAvialible(int userId,int productId, DateTime from, DateTime to)
        {

            var query = from o in context.Orders
                        join i in context.ItemsForOrders on o.Id equals i.OrderId
                        join p in context.Products on i.ProductId equals p.Id
                        select new
                        { o.Id, o.FromDate, o.ToDate, i.ProductId, p.Type };
            var results = query.ToList();
            if (results.Any(res => res.ProductId == productId
           && res.FromDate>=from && res.ToDate <= to))
            {
                return -1;
            }
           
            return AddToCart(userId,productId,from,to);
        }
        public int AddToCart(int userId,int productId, DateTime from, DateTime to)
        {
            Models.Cart? existingCart = context.Carts.FirstOrDefault(cart => cart.UserId == userId && cart.IsOpen == true);

            if (existingCart == null)
            {
                Models.Cart newCart = new Cart();
                newCart.UserId = userId;
                newCart.IsOpen = true;
                newCart.FromDate= from;
                newCart.ToDate= to;
                context.Carts.Add(newCart);
                context.SaveChanges();
                existingCart = newCart; // Assign the newCart object to existingCart
            }

            if (!context.CartProducts.Where(cp => cp.CartId == existingCart.Id)
                .Any(cp => cp.ProductId == productId))
            {
                Models.CartProduct cartProduct = new Models.CartProduct()
                {
                    ProductId = productId
                };
                existingCart.CartProducts.Add(cartProduct);
                existingCart.TotalPrice += context.Products.First(p => p.Id == productId).Price;
                context.SaveChanges();
                return existingCart.Id;
            }
            else
            {
                return -2;
            }
        }

        //public int AddToCart(int userId, int productId)
        //{
        //    Models.Cart? existingCart = context.Carts.FirstOrDefault(cart => cart.UserId == userId && cart.IsOpen == true);

        //    if (existingCart == null)
        //    {
        //        Models.Cart newCart = new Cart();
        //        newCart.UserId = userId;
        //        newCart.IsOpen = true;
        //        context.Carts.Add(newCart);
        //        context.SaveChanges();
        //        existingCart = newCart; // Assign the newCart object to existingCart
        //    }

        //    if (!context.CartProducts.Where(cp => cp.CartId == existingCart.Id)
        //        .Any(cp => cp.ProductId == productId))
        //    {
        //        Models.CartProduct cartProduct = new Models.CartProduct()
        //        {
        //            ProductId = productId
        //        };
        //        existingCart.CartProducts.Add(cartProduct);
        //        existingCart.TotalPrice += context.Products.First(p => p.Id == productId).Price;
        //        context.SaveChanges();
        //        return existingCart.Id;
        //    }
        //    else
        //    {
        //        return -1;
        //    }     
        //}
        public bool Delete(int id)
        {
            return true;
        }
        public double GetTotalPrice(int cartId)
        {
            var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart != null)
                return cart.TotalPrice;
            return 0;
        }
    }
}
