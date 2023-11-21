using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartService
    {
        public DTO.Cart Get(int id);

        public int ProductIsAvialible(int userId, int productId, DateTime from, DateTime to);
        //public int AddToCart(int userId, int productId);
        public bool Delete(int id);
        public double GetTotalPrice(int cartId);
    }
}
