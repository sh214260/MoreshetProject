﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IProductRpository
    {
        public bool AddNew(Models.Product newProduct);
        public IEnumerable<Models.Product> Get(Func<Models.Product, bool>? predicate = null);
    }
}
