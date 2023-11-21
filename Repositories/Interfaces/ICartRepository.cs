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

        //public int AddToCart(int userId, int productId);
        public int ProductIsAvialible(int userId,int productId, DateTime from, DateTime to);
        public bool Delete(int id);
        public double GetTotalPrice(int cartId);


    }
}
