﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface  IUserService
    {
        bool AddNew(DTO.User newUser);
        public DTO.User Get(int id);
        public void Delete(int userId);
        public IEnumerable<DTO.User> Get(Func<Repositories.Models.User, bool>? predicate = null);
    }
}
