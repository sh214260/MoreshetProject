using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        public bool AddNew(Product product);
        
        public void Delete(int productId);
        public DTO.Product Get(int id); 
        public IEnumerable<DTO.Product> Get(Func<Repositories.Models.Product, bool>? predicate = null);
    }
}
