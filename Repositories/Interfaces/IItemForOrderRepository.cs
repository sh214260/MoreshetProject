using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IItemForOrderRepository
    {
        public IEnumerable<Models.Product> GetProducts(int orderId);
    }
}
