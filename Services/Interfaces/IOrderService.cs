using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        public int AddNew(DTO.Order newOrder);
        public DTO.Order Get(int orderId);
        public void Delete(int orderId);
        public IEnumerable<DTO.Order> Get(Func<Repositories.Models.Order, bool>? predicate = null);
    }
}
