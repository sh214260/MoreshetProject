using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        public bool SendContactFormEmail(string? name, string? email, string? phone, string? message);
    }
}
