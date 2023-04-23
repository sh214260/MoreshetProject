using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository:IProductRpository
    {
        private readonly FullStackMoreshetdbContext context;
        public ProductRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }
    }
}
