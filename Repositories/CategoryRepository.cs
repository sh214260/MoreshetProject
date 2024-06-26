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
        public bool AddNew(Category newCategory)
        {
            try
            {
                context.Categories.Add(newCategory);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int categoryId)
        {
            Models.Category category = new Category();
            category = context.Categories.Find(categoryId);
            context.Categories.Remove(category);
            context.SaveChanges();
        }
        public Repositories.Models.Category? Get(int id)
        {
            Repositories.Models.Category? category = context.Categories?.FirstOrDefault(cat=>cat.Id == id);
            if (category!=null)
                return category;
            return null;
        }
        public IEnumerable<Category> Get(Func<Category, bool>? predicate = null)
        {
            if (predicate == null)
            {
                return context.Categories.ToList();
            }
            return context.Categories.Where(predicate);
        }
    }
}
