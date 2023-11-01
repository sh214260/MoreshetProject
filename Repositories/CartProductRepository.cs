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
            var query = from c in context.Carts
                        join cp in context.CartProducts on c.Id equals cp.CartId
                        join p in context.Products on cp.ProductId equals p.Id
                        select new
                        { cp.ProductId };
            var results = query.ToList();
            var filteredProducts = context.Products.Where(p => results.Any(r => r.ProductId == p.Id));
            return filteredProducts;
        }
    }
}
