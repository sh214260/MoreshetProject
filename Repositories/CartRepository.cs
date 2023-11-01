using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository:ICartRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public CartRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }
        public bool ProductIsAvialible(string type, DateTime from, DateTime to)
        {
            var query = from o in context.Orders
                        join i in context.ItemsForOrders on o.Id equals i.OrderId
                        join p in context.Products on i.ProductId equals p.Id
                        select new
                        { o.Id, o.FromDate, o.ToDate, i.ProductId, p.Type };
            var results = query.ToList();
            if (results.Any(res => res.Type == type
            && res.FromDate >= from && res.ToDate <= to))
            {
                return false;
            }               
            return true;
        }
        public Models.Cart AddToCart(int userId, int productId)
        {
            Models.Cart existingCart = context.Carts.FirstOrDefault(cart => cart.UserId == userId && cart.IsOpen == true);
            if (existingCart == null)
            {
                Models.Cart newCart = new Cart();
                newCart.UserId = userId;
                newCart.IsOpen= true;
                context.Carts.Add(newCart);
                context.SaveChanges();
                Models.CartProduct cartProduct = new Models.CartProduct()
                {
                    ProductId = productId,
                    CartId = newCart.Id
                };
                newCart.CartProducts.Add(cartProduct);
                //context.SaveChanges();
                return newCart;
            }
            else
            {
                Models.CartProduct cartProduct = new Models.CartProduct()
                {
                    ProductId = productId,
                    CartId = existingCart.Id
                };
                existingCart.CartProducts.Add(cartProduct);
                context.SaveChanges();
                return existingCart;
            }
        }
        public int GetTotalPrice(int cartId)
        {
            var query = from c in context.Carts
                        join cp in context.CartProducts on c.Id equals cp.CartId
                        join p in context.Products on cp.ProductId equals p.Id
                        select new
                        { cp.ProductId, p.Price };
            var results = query.ToList();
            int totalPrice = results.Sum(p => p.Price);
            return totalPrice;
        }
    }
}
