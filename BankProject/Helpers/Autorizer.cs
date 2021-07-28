using System.Collections.Generic;
using System.Linq;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
   public class Autorizer
    {
        /// <summary>
        /// Create a new user account
        /// </summary>
        /// <param name="login">login for creating account</param>
        /// <param name="password">password for creating account</param>
        /// <returns>created user</returns>
        public static User CreateAccount(string login, string password)
        {


            var bankAccount = new List<BaseAccount> {new BaseAccount()};
            using var context = new BankDbContext();
            var user = new User()
            {
                Login = login,
                Password = password,
               BankAccounts =bankAccount
              

            };
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        /// <summary>
        /// searches the database for a user with the specified input parameters
        /// </summary>
        /// <param name="login">login for searching</param>
        /// <param name="password">password for searching</param>
        /// <returns>the user if there is one</returns>
        public static User TryGetUser(string login, string password)
        {
            using var context = new BankProject.Helpers.BankDbContext();
            return context.Users.Include(u=>u.BankAccounts)
                .ThenInclude(u=>u.Cards)
                .Include(u=>u.PersonalInfo)
                .FirstOrDefault(u => u.Login == login&& u.Password==password);
        }

    }
}
