using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRpository
    {
        private readonly FullStackMoreshetdbContext context;
        public ProductRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public bool AddNew(Models.Product newProduct)
        {
            try
            {
                if (context.Products.Any(p => p.Name == newProduct.Name))
                {
                    return false;
                }
                context.Products.Add(newProduct); 
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Models.CartProduct AddToCart(int userId, int productId)
        {
            Models.Cart existingCart = context.Carts.FirstOrDefault(cart => cart.UserId == userId && cart.IsOpen==true);
            
            if (existingCart == null)
            {
                Models.Cart newCart = new Cart();
                newCart.UserId = userId;
                context.Carts.Add(newCart);
                Models.CartProduct cartProduct = new Models.CartProduct() 
                { 
                  ProductId = productId,
                  CartId=newCart.Id
                };
                newCart.CartProducts.Add(cartProduct);
                return cartProduct;
            }
            else
            {
                Models.CartProduct cartProduct = new Models.CartProduct()
                {
                    ProductId = productId,
                    CartId = existingCart.Id
                };
                existingCart.CartProducts.Add(cartProduct);
                return cartProduct;
            }
            
        }


        public void Delete(int productId)
        {
            try
            {
                if (productId < 0)
                {
                    //to do: ex
                    throw new ArgumentOutOfRangeException();
                }
                Models.Product product = new Product();
                product = context.Products.Find(productId);
                context.Products.Remove(product);
                context.SaveChanges();

            }
            catch
            {

            }
        }
        public Product Get(int id)
        {
            if (id < 0)
            {
                //to do: ex
                throw new ArgumentOutOfRangeException();
            }
            Models.Product product = new Product();
            product = context.Products.Find(id);
            return product;

        }
        public IEnumerable<Models.Product> Get(Func<Models.Product, bool>? predicate = null)
        {
            if(predicate == null)
            {
                return context.Products.ToList();
            }
            return context.Products.Where(predicate);
        }

    }
}
