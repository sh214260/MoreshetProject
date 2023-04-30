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
    public class UserRepository : IUserRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public UserRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public bool AddNew(User newUser)
        {
            if (newUser == null)
            {
                //to do: nulL exception
                throw new ArgumentNullException();
            }
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
            if (id < 0)
            {
                //to do: ex
                throw new ArgumentOutOfRangeException();
            }
            Models.User user = new User();
            user = context.Users.Find(id);
            return user;
        }
        public void Delete(int userId)
        {
            try
            {
                if (userId < 0)
                {
                    //to do: ex
                    throw new ArgumentOutOfRangeException();
                }
                Models.User user = new User();
                user = context.Users.Find(userId);
                context.Users.Remove(user);
                context.SaveChanges();
                
            }
            catch
            {
                
            }
        }
        public IEnumerable<Models.User> Get(Func<Models.User, bool>? predicate = null)
        {
            if (predicate == null)
            {
                return context.Users.ToList();
            }
            return context.Users.Where(predicate);
        }
    }
}
