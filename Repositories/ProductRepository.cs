using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository:IProductRpository
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

        public IEnumerable<Models.Product> Get(Func<Models.Product, bool>? predicate = null)
        {
            return context.Products.ToList();
        }
    }
}
