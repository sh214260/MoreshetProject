using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        public bool AddNew(DTO.Category newCategory);
        public void Delete(int categoryId);
        public IEnumerable<DTO.Category> Get(Func<Repositories.Models.Category, bool>? predicate = null);
    }
}
