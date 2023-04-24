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
    }
}
