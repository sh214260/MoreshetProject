using Repositories.Interfaces;
using Repositories.Models;
using System;

namespace Repositories
{
    public class CartProductRepository:ICartProductRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public CartProductRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
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
                Models.Cart? cart = context.Carts.FirstOrDefault(c => c.Id == cartId);
                if (cart != null)
                {
                    var cartProduct = context.CartProducts.FirstOrDefault
                        (cp => cp.CartId == cartId && cp.ProductId == productId);
                    if (cartProduct != null)
                    {
                        context.CartProducts.Remove(cartProduct);
                        Models.Product pr = context.Products.First(p => p.Id == productId);
                        cart.TotalPrice -= pr.Price;
                        TimeSpan? time = (cart.ToDate - cart.FromDate);
                        double numOfAdditionHours = 0;
                        numOfAdditionHours = time.Value.TotalHours-4;
                        int priceForAdditionHours = 0;
                        if (numOfAdditionHours > 0)
                        {
                            priceForAdditionHours = (int)pr.Price / 8;
                            cart.TotalPrice -= priceForAdditionHours * numOfAdditionHours;
                        }
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }
        public IEnumerable<Models.CartProduct> Get(int cartId)
        {
            return context.CartProducts.Where(ca => ca.CartId == cartId).ToList();
        }

    }
}
