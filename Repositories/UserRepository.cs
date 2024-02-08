using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FullStackMoreshetdbContext context;
        public UserRepository(FullStackMoreshetdbContext dal)
        {
            this.context = dal;
        }

        public bool AddNew(string password,User newUser)
        {
            if (newUser == null)
            {
                //to do: nulL exception
                throw new ArgumentNullException();
            }
            try
            {
                if (context.Users.Any(u => u.Email == newUser.Email))
                {
                    return false;
                }
                newUser.Password=password;
                context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
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

        public Models.User GetUser(string email, string password)
        {
            //todo:first or defual
            Models.User ?user = context.Users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();
            if (user != null)
                return user;
            return null;
            
        }
        public Models.User GetUserByPhone(string phonenumber)
        {
            //todo:first or defual
            Models.User? user = context.Users.Where(user => user.PhoneNumber1 == phonenumber || user.PhoneNumber2 == phonenumber).FirstOrDefault();
            if (user != null)
                return user;
            return null;

        }
        public bool UpdateAddress(int userId, string address)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Address = address;
                context.Entry(user).Property(u => u.Address).IsModified = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateUser(Models.User newUser)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == newUser.Id);

            if (user != null)
            {
                //context.Users.Update(newUser);
                user.Email= newUser.Email;
                user.Address= newUser.Address;
                user.PhoneNumber1= newUser.PhoneNumber1;
                user.PhoneNumber2= newUser.PhoneNumber2;
                user.Name= newUser.Name;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public User GetUserByToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
