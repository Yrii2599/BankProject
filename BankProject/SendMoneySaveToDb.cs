using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;

namespace BankProject.ConsoleInterfaces
{
    class SendMoneySaveToDb
    {
        public static void SendMoneyUpdateDb(User user)
        {
            string toAccount;
            int count;
            string fromAccount;
            SendMoneyInterface.SendInterface(out toAccount, out count, out fromAccount);
            using (var context = new BankDbContext())
            {
                var result = context.BankAccounts.FirstOrDefault(u => u.Account == fromAccount);
                if (result != null)
                {
                    result.SendMoney(toAccount, count);
                    context.SaveChanges();
                }
            }
        }
    }
}
