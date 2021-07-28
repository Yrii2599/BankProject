using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
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

        [Fact]
        public void IsMoneySendToAnotherAccount()
        {
            BaseAccount fromAccount = new BaseAccount() { Account = "61327227081409325202339", Balance = 1000 };
            BaseAccount toAccount = new BaseAccount() { Account = "07612768422552951321108", Balance = 1000 };
            
            fromAccount.SendMoney(toAccount.Account,500);
            using var context = new BankDbContext();
            var str = context.BankAccounts.FirstOrDefault(u => u.Account == toAccount.Account);
            Assert.Equal(500,fromAccount.Balance);
            Assert.Equal(1500,toAccount.Balance);
        }
    }
}
