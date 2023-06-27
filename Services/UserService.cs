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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Services
{
    public class UserService : Interfaces.IUserService
    {
        private readonly IMapper mapper;
        private readonly Repositories.Interfaces.IUserRepository repository;
        public UserService(IUserRepository dal, IMapper _mapper)
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
            if (id < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            try
            {
                Repositories.Models.User user1 = repository.Get(id);
                DTO.User user;
                user = mapper.Map<DTO.User>(user1);
                return user;
            }
            catch
            {
                //to do: ex 
                throw;
            }
            

        }

        public void Delete(int userId)
        {
            if (userId < 0)
            {
                throw new EntityNotFoundExceptions();
            }
            try
            {
                repository.Delete(userId);
            }
            catch
            {
                throw new ArgumentNullException(nameof(userId));    
            }
        }

        public IEnumerable<User> Get(Func<Repositories.Models.User, bool>? predicate = null)
        {
            IEnumerable<Repositories.Models.User> ModelsUser = repository.Get(predicate);
            if (ModelsUser == null)
            {
                return null;
            }
            IEnumerable<DTO.User> users = ModelsUser.Select(ord => mapper.Map<Repositories.Models.User, DTO.User>(ord));
            if (users == null)
            {
                throw new EmptyListException();
            }
            return users;
        }

        public User GetUser(string email, string password)
        {
            Repositories.Models.User user1 = repository.GetUser(email, password);
            DTO.User user;
            user = mapper.Map<DTO.User>(user1);
            return user;
            
        }
    }
}
