using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {

        public bool AddNew(Category newCategory);
        public void Delete(int categoryId);
        public Repositories.Models.Category Get(int id);
        public IEnumerable<Category> Get(Func<Models.Category, bool>? predicate = null);
    }
}
