using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool AddNew(Models.User newUser);
        public Models.User Get(int id);
        public void Delete(int userId);
    }
}
