using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.ConsoleInterfaces;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class MoneyOperationToDb
    {
        public static void AddMoney(User user)
        {
            string accountNumber;
            int count;
            AddMoneyMenu.Menu(out accountNumber, out count);
            using (var context = new BankDbContext())
            {
                var result = context.BankAccounts.FirstOrDefault(u => u.Account == accountNumber && u.User.Id == user.Id);
                if (result != null)
                {
                    result.Balance += count;
                    context.SaveChanges();
                    Console.WriteLine("Operation success");
                }
                else
                {
                    Console.WriteLine($"You can only top up your accounts");
                }
            }
        }
        public static void WidthrawMoney (User user)
        {
            string accountNumber;
            int count;
            AddMoneyMenu.Menu(out accountNumber, out count);
            using (var context = new BankDbContext())
            {
                var result = context.BankAccounts.FirstOrDefault(u => u.Account == accountNumber && u.User.Id == user.Id);
                if (result != null)
                {
                    result.Balance -= count;
                    context.SaveChanges();
                    Console.WriteLine("Operation success");
                }
                else
                {
                    Console.WriteLine($"You can only withdraw funds from your account");
                }
            }
        }

        public static void AccountInfo(User user)
        {
            using (var context = new BankDbContext())
            {

                var std = context.Users.Include(u=>u.BankAccounts)
                    .FirstOrDefault(u => user.Id == u.Id)?.BankAccounts;
                foreach (var account in std)
                {
                    Console.WriteLine($"Account:\t {account.Account}");
                    Console.WriteLine($"Type:\t\t {account.Type}");
                    Console.WriteLine($"Balance:\t {account.Balance}");
                }
                    
            }
        }
    }
}
