using DTO;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace Services
{
    public  class UserService : Interfaces.IUserService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.IUserRepository repository;
        public UserService(IUserRepository dal,IMapper _mapper)
        {
            mapper = _mapper;
            repository = dal;
        }
        public bool AddNew(DTO.User newUser)
        {
            if (newUser != null)
            {
                repository.AddNew(mapper.Map<Repositories.Models.User>(newUser));
                return true;
            }
            return false;
        }

        public DTO.User Get(int id)
        {
            DTO.User user;
            user=mapper.Map<DTO.User>(repository.Get(id));
            return user;
            
        }

        public void Delete(int userId)
        {
           repository.Delete(userId);
        }
    }
}
