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
        bool AddNew(Product product);
        public IEnumerable<DTO.Product> Get(Func<DTO.Product, bool>? predicate = null);
    }
}
