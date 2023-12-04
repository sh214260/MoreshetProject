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
        public Cart GetByUser(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Models.Cart? cart = context.Carts.FirstOrDefault(cart => cart.UserId == id && cart.IsOpen == true);
            if (cart == null)
            {
                Models.Cart newCart = new Models.Cart()
                {
                    UserId = id,
                    IsOpen = true
                };
                context.Carts.Add(newCart);
                context.SaveChanges();
                return newCart;
            }
            return cart;
        }
        public List<int> ProductIsAvialible(string productType, DateTime fromDate, DateTime to)
        {
            List<int> results = new List<int>();
            var query1 = from o in context.Orders
                         join i in context.ItemsForOrders on o.Id equals i.OrderId
                         join p in context.Products on i.ProductId equals p.Id
                         where p.Type == productType
                         select p.Id;
            List<int> productsNotInOrders = context.Products.Where(p => p.Type == productType)
                .Where(p => !query1.Contains(p.Id)).Select(p => p.Id).ToList();

            var query2 = from o in context.Orders
                         join i in context.ItemsForOrders on o.Id equals i.OrderId
                         join p in context.Products on i.ProductId equals p.Id
                         
                         where p.Type == productType
                         //why it is not working
                         where (o.FromDate > to || o.ToDate < fromDate)
                         select p.Id;
            List<int> productsInOrderDate = query2.ToList();
            results.AddRange(productsNotInOrders);
            results.AddRange(productsInOrderDate);
            return results;
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
                //צריך לבדוק שכאשר משנים תאריך כל המוצרים בעגלה עדיין פנויים בתאריך החדש!
                existingCart.FromDate= from;
                existingCart.ToDate= to;
                existingCart.CartProducts.Add(cartProduct);
                existingCart.TotalPrice += context.Products.First(p => p.Id == productId).Price;
                context.SaveChanges();
                return existingCart.Id;
            }
            else
            {
                //קיים כבר מוצר כזה
                return -1;
            }
        }
        public bool UpdateDate(int cartId, DateTime from, DateTime to)
        {
            var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);

            if (cart != null)
            {
                cart.FromDate = from;
                cart.ToDate=to;
                context.Entry(cart).Property(c => c.FromDate).IsModified = true;
                context.Entry(cart).Property(c => c.ToDate).IsModified = true;
                context.SaveChanges();
                return true;
            }
            return false;
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
