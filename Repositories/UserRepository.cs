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
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
