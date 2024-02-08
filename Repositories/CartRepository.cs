using Microsoft.EntityFrameworkCore;
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
        public List<int> ProductIsAvialible(int productId, string productType, DateTime fromDate, DateTime to)
        {
            var query1 = from o in context.Orders
                         join i in context.ItemsForOrders on o.Id equals i.OrderId
                         join p in context.Products on i.ProductId equals p.Id
                         where p.Type == productType
                         select o;
            List<Order> test = query1.ToList();
            Console.WriteLine(test);


            List<int> results = new List<int>();
            var query2 = from o in context.Orders
                         join i in context.ItemsForOrders on o.Id equals i.OrderId
                         join p in context.Products on i.ProductId equals p.Id
                         where p.Type == productType
                         where ( (o.FromDate <= fromDate && o.ToDate >= to)
                         || (o.FromDate >= fromDate && o.FromDate < to)
                         || (o.ToDate >= fromDate && o.ToDate < to))                         
                         select p.Id;
            List<int> productsTypeInOrder = query2.ToList();
            if (productsTypeInOrder.Contains(productId))
            {
                //נצטרך לחפש מוצר אחר
                List<int> allproductstype = context.Products.Where(p => p.Type == productType).Select(p => p.Id).ToList();
                allproductstype.RemoveAll(p => productsTypeInOrder.Contains(p));
                results.AddRange(allproductstype);
            }
            else
            {
                results.Add(productId);
            }
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
                newCart.FromDate = from;
                newCart.ToDate = to;
                context.Carts.Add(newCart);
                context.SaveChanges();
                existingCart = newCart;
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
            var cart = context.Carts.Include(c => c.CartProducts).FirstOrDefault(c => c.Id == cartId);

            if (cart != null)
            {
                //var prod = cart.CartProducts;
                var prod = context.CartProducts.Where(cp => cp.CartId == cart.Id);
                context.CartProducts.RemoveRange(prod);
                cart.FromDate = from;
                cart.ToDate = to;
                cart.TotalPrice = 0;
                context.SaveChanges();
                return true;
            }

            return false;
        }

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
