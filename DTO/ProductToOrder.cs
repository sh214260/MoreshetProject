using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductToOrder
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public DateTime From { get; set; }  
        public DateTime To { get; set; }
    }
}
