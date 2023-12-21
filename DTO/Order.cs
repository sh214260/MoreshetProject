using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int DeliveryPrice { get; set; }

        public DateTime DateOrder { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool PaidUp { get; set; }

        public bool? Receipt { get; set; }

        public int TotalPrice { get; set; }
        public int? CartId { get; set; }

    }
}
