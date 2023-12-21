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
        public List<Models.Product> GetAvailable(DateTime fromDate, DateTime to)
        {
            List<Product> results = new List<Product>();
            var query1 = from o in context.Orders
                         join i in context.ItemsForOrders on o.Id equals i.OrderId
                         join p in context.Products on i.ProductId equals p.Id
                         where o.FromDate<=fromDate && o.ToDate>=to
                         select p;
            List<Product> productsNotInOrders = context.Products
                .Where(pro => !query1.Contains(pro)).ToList();
            results.AddRange(productsNotInOrders);
            return results;
        }
    }
}
