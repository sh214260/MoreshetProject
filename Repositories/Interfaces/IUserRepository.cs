using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool AddNew(string password, Models.User newUser);
        public Models.User Get(int id);
        public void Delete(int userId);
        public IEnumerable<Models.User> Get(Func<Models.User, bool>? predicate = null);
        public Models.User GetUser(string email, string password);
        public bool UpdateAddress(int userId, string address);
        public bool UpdateUser(Models.User newUser);
    }
}
