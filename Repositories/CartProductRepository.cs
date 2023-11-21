using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartProductRepository:ICartProductRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public CartProductRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public IEnumerable<Models.CartProduct> Get(int cartId)
        {
            return context.CartProducts.Where(ca=>ca.CartId==cartId).ToList();
        }
        public IEnumerable<Models.Product> GetProducts(int cartId)
        {
            var filteredProducts = context.Products
                .Join(
                    context.CartProducts,
                    p => p.Id,
                    cp => cp.ProductId,
                    (p, cp) => new { Product = p, CartId = cp.CartId }
                )
                .Where(x => x.CartId == cartId)
                .Select(x => x.Product)
                .ToList();

            return filteredProducts;
        }
        public bool Delete(int cartId, int productId)
        {
            if (cartId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                var cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart != null)
                {
                    var cartProduct = context.CartProducts.FirstOrDefault
                        (cp => cp.CartId == cartId && cp.ProductId == productId);
                    if (cartProduct != null)
                    {
                        context.CartProducts.Remove(cartProduct);
                        cart.TotalPrice -= context.Products.First(p => p.Id == productId).Price;
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
