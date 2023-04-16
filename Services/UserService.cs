using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Services
{
    public  class UserService : Interfaces.IUserService
    {
       
        public bool AddNew(User newUser)
        {
            //todo: use dal layer to insert new user
            //return true or false - determined if the action succeeded
            //use it by DI !!!!
            throw new NotImplementedException();
        }
    }
}
