using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Address { get; set; }

        public string PhoneNumber1 { get; set; }
        public string? PhoneNumber2 { get; set; }

        public int? Type { get; set; }

       
    }

}
