using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICartProductRepository
    {
        public IEnumerable<Models.CartProduct> Get(int cartId);
        public IEnumerable<Models.Product> GetProducts(int cartId);
        public bool Delete(int cartId, int productId);
    }
}
