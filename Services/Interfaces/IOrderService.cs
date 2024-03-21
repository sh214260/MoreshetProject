using DTO;
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
        public bool Delete(int orderId);
        public IEnumerable<DTO.Order> Get(Func<Repositories.Models.Order, bool>? predicate = null);
        public IEnumerable<Order> GetByUser(int userId);
        public IEnumerable<OrderByDay> GetByDate(DateOnly date);
        public int GetDeliveryPrice(int cartId);
        public OrderData GetAllData(int orderId);
    }
}
