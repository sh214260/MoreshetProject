using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Models.Cart Get(int id);
        public Models.Cart GetByUser(int id);
        public int AddToCart(int userId, int productId, DateTime from, DateTime to);
        public List<int> ProductIsAvialible(int productId,string productType, DateTime from, DateTime to);
        public bool UpdateDate(int cartId, DateTime from, DateTime to);
        public bool Delete(int id);
        public double GetTotalPrice(int cartId);

    }
}
