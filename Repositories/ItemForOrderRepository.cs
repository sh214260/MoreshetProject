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
        
    }
}
