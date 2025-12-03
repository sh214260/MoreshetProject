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
        public DTO.Product Get(int id);
        public IEnumerable<DTO.Product> Get(Func<Repositories.Models.Product, bool>? predicate = null);
        public List<DTO.Product> GetAvailable(DateTime from, DateTime to);
        public bool Delete(int productId);
        public IEnumerable<string> GetImages();
        public bool Update(Product product);
    }
}
