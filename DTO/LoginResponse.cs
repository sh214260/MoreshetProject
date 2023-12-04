using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginResponse
    {
       public User User { get; set; }
       public Cart? Cart { get; set; }
       public IEnumerable<Product>? CartProducts { get; set;  }
    }
}
