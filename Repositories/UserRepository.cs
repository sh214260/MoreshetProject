using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public  class UserRepository : IUserRepository
    {
        private readonly  FullStackMoreshetdbContext context;
        public UserRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public bool AddNew(User newUser)
        {
            try
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Repositories.Models.User Get(int id)
        {
            Models.User user = new User();
            user = context.Users.Find(id);
            return user;
        }
        public void Delete(int userId)
        {
            Models.User user = new User();
            user = context.Users.Find(userId);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
