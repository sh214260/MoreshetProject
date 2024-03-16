using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderData
    {
        public DTO.Order order { get; set; }
        public DTO.User user { get; set; }
        public IEnumerable<DTO.Product> products { get; set; }
    }
}
