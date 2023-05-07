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
                context.Products.Add(newProduct);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
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
