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
        public bool Delete(int productId);
        public Models.Product Get(int id);
        public IEnumerable<Models.Product> Get(Func<Models.Product, bool>? predicate = null);
        public List<Product> GetAvailable(DateTime from, DateTime to);
        public IEnumerable<string> GetImages();
        public bool Update(Models.Product product);
    }
}
