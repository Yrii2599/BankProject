using System;
using System.Linq;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class BaseAccount : BankAccount
    {
        public BaseAccount()
        {
            var random = new Random();
            Type = "default";
            Balance = 0;
            string bankNumber = "";
            for (int i = 1; i < 24; i++)
            {

                bankNumber += random.Next(0, 10);
            }

            Account = bankNumber;
        }

        public void AddToDB(int id)
        {
            using var context = new BankDbContext();
            var res = context.Users.Include(u => u.BankAccounts)
                .FirstOrDefault(u => u.Id == id)?.BankAccounts;
            if (res != null)
            {
                res.Add(this);
                context.SaveChanges();
            }
        }

        public void SendMoney(string account, int count)
        {
            if (this.Balance > count)
            {
                using (var context = new BankDbContext())
                {
                    var a = context.BankAccounts.ToList();
                    var res = context.BankAccounts.FirstOrDefault(u => u.Account == account);
                    if (res != null)
                    {
                        this.Balance -= count;
                        res.Balance += count;
                        Console.WriteLine($"Operation success");
                        context.SaveChanges();
                    }
                }

            }
            else
            {
                Console.WriteLine($"You have not enough money");
            }
        }

    }
}
