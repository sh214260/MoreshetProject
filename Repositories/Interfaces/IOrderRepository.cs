﻿using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public int AddNew(Models.Order newOrder);
        public Models.Order Get(int orderId);
        public bool Delete(int orderId);
        public IEnumerable<Models.Order> Get(Func<Models.Order, bool>? predicate = null);
        public IEnumerable<Models.Order> GetByUser(int userId);
        public IEnumerable< Order> GetByDate(DateOnly date);
        public int GetDeliveryPrice(int cartId);
        public List<string> GetProductsName(int orderId);
    }
}
