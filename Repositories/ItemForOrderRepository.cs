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

        public bool Delete(int orderId, int productId)
        {
            if (orderId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                var order = context.Orders.FirstOrDefault(c => c.Id == orderId);
                if (order != null)
                {
                    var itemProduct = context.ItemsForOrders.FirstOrDefault
                        (cp => cp.OrderId == orderId && cp.ProductId == productId);
                    if (itemProduct != null)
                    {
                        context.ItemsForOrders.Remove(itemProduct);
                        Models.Product pr = context.Products.First(p => p.Id == productId);
                        order.TotalPrice -= pr.Price;
                        TimeSpan? time = (order.ToDate - order.FromDate);
                        double numOfAdditionHours = 0;
                        numOfAdditionHours = time.Value.TotalHours-4;
                        int priceForAdditionHours = 0;
                        if (numOfAdditionHours > 0)
                        {
                            priceForAdditionHours = (int)pr.Price / 8;
                            order.TotalPrice -= priceForAdditionHours * (int)numOfAdditionHours;
                        }
                        if (order.TotalPrice <= 0)
                        {
                            context.Orders.Remove(order);
                        }
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
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
