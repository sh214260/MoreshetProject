using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Models;
using Repositories.Interfaces;
namespace Services.Interfaces
{
    public interface IItemForOrderService
    {
        public bool Delete(int cartId, int productId);
        public IEnumerable<DTO.Product> GetProducts(int orderId);
    }
}
