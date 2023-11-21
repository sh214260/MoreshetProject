using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRpository
    {
        public bool AddNew(Models.Product newProduct);
        public void Delete(int productId);
        public Models.Product Get(int id);
        public IEnumerable<Models.Product> Get(Func<Models.Product, bool>? predicate = null);
        IEnumerable<Product> GetAvailable(DateTime from, DateTime to);
    }
}
