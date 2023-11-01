using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Models.Cart AddToCart(int userId, int productId);
        public bool ProductIsAvialible(string type, DateTime from, DateTime to);
        public int GetTotalPrice(int cartId);

    }
}
