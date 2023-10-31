using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductToOrder
    {
        public string Type { get; set; }
        public DateTime from { get; set; }  
        public DateTime to { get; set; }
    }
}
