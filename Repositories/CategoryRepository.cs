﻿using Repositories.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public CategoryRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }
    }
}
