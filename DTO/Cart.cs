using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public double TotalPrice { get; set; }

        public bool IsOpen { get; set; }

    }
}
