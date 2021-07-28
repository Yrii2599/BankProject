using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BankProject.Tests
{
   public class AccountTests
    {
        [Fact]
        public void IsAccountAddToDb()
        {
            BaseAccount account = new BaseAccount() { Account = "61327227081409325202339", Balance = 5000 };
            account.AddToDB(1);
            using var context = new BankDbContext();
            var str = context.BankAccounts.FirstOrDefault(u => u.Id == 1);
            if (str != null)
            {
                Assert.Equal(account.Account, str.Account);
            }
        }

        [Theory]
        [InlineData("Allaa", "1", 150, 500)]
        [InlineData("Vasia", "123", 300, 1000)]
        [InlineData("Zhenia", "2536", 256, 400)]
        public void IsMoneySendToAnotherAccount(string name, string password, int count, int balance)
        {
            var user = new User() { BankAccounts = new List<BaseAccount>(), Login = name, Password = password };
            BaseAccount accountFrom = new BaseAccount();
            BaseAccount accountTo = new BaseAccount();
            accountFrom.Balance = balance;
            accountFrom.User = user;
            accountTo.Balance = balance;
            accountTo.User = user;
            user.BankAccounts.Add(accountFrom);
            user.BankAccounts.Add(accountTo);


            using (var context = new BankDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            BaseAccount fromAccount;
            BaseAccount toAccount;
            using (var context = new BankDbContext())
            {
                fromAccount = context.BankAccounts.FirstOrDefault(u => u.Account==accountFrom.Account);
                toAccount = context.BankAccounts.FirstOrDefault(u => u.Account == accountTo.Account);
            }

            if (fromAccount != null && toAccount != null)
            {
                fromAccount.SendMoney(toAccount.Account, count);


            }
            using (var context = new BankDbContext())
            {
                fromAccount = context.BankAccounts.FirstOrDefault(u => u.Account == accountFrom.Account);
                toAccount = context.BankAccounts.FirstOrDefault(u => u.Account == accountTo.Account);
            }

            if (fromAccount != null && toAccount != null)
            {
                Assert.Equal(expected: balance - count, actual: fromAccount.Balance);
                Assert.Equal(expected: balance + count, actual: toAccount.Balance);
            }

            using (var context = new BankDbContext())
            {
                context.Remove(user);
                context.SaveChanges();
            }




        }
    }
}
