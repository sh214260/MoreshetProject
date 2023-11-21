using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartProductService
    {
        public IEnumerable<DTO.CartProduct> Get(int cartId);
        public IEnumerable<DTO.Product> GetProducts(int cartId);
        public bool Delete(int cartId, int productId);
    }
}
