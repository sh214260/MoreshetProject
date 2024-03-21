using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IItemForOrderRepository
    {
        public bool Delete(int orderId, int productId);
        public IEnumerable<Models.Product> GetProducts(int orderId);
    }
}
