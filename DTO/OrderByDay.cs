using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderByDay
    {
        public int OrderId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? UserName { get; set; }
        public List<string>? ProductsName { get; set; } 

    }
}
