using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICartService
    {
        
       public bool ProductIsAvialible(string type, DateTime from, DateTime to);
        public DTO.Cart AddToCart(int userId, int productId);
    }
}
