using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.ICategoryRepository repository;
        public CategoryService(ICategoryRepository dal, IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }
        public bool AddNew(DTO.Category newCategory) 
        {
            if (newCategory != null)
            {
                repository.AddNew(mapper.Map<Repositories.Models.Category> (newCategory)) ;
                return true;
            }
            return false;
        }

        public void Delete(int categoryId)
        {
            repository.Delete(categoryId);
        }
        public DTO.Category Get(int id)
        {
            return mapper.Map<Repositories.Models.Category, DTO.Category>(repository.Get(id));
        }
        public IEnumerable<DTO.Category> Get(Func<Repositories.Models.Category, bool>? predicate = null)
        {
            IEnumerable<Repositories.Models.Category> ModelsCategory = repository.Get(predicate);
            if (ModelsCategory == null)
            {
                return null;
            }
            IEnumerable<DTO.Category> categories = ModelsCategory.Select(cat => mapper.Map<Repositories.Models.Category, DTO.Category>(cat));
            if (categories == null)
            {
                throw new EmptyListException();
            }
            return categories;
        }
    }
}
