using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class Autorize
    {

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

        public static User TryGetUser(string login, string password)
        {
            using var context = new BankProject.Helpers.BankDbContext();
            var result = context.Users.ToList();
            return context.Users.Include(u=>u.BankAccounts)
                .ThenInclude(u=>u.Cards)
                .Include(u=>u.PersonalInfo)
                .FirstOrDefault(u => u.Login == login&& u.Password==password);
        }

    }
}
