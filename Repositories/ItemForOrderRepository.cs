using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ItemForOrderRepository:IItemForOrderRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public ItemForOrderRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }
        public IEnumerable<Models.Product> GetProducts(int orderId)
        {
            var filteredProducts = context.Products
                .Join(
                    context.ItemsForOrders,
                    p => p.Id,
                    oitem => oitem.ProductId,
                    (p, oitem) => new { Product = p, orderId = oitem.OrderId }
                )
                .Where(x => x.orderId == orderId)
                .Select(x => x.Product)
                .ToList();

            return filteredProducts;
        }

    }
}
