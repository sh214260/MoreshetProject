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
        public DTO.Cart GetByUser(int id);
        public int AddToCart(int userId, int productId, DateTime from, DateTime to);
        public List<int> ProductIsAvialible(string productType, DateTime from, DateTime to);
        //public int AddToCart(int userId, int productId);
        public bool UpdateDate(int cartId, DateTime from, DateTime to);
        public bool Delete(int id);
        public double GetTotalPrice(int cartId);
    }
}
