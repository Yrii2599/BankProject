using System;
using System.Linq;
using BankProject.Abstraction;

using BankProject.Helpers;
using BankProject.States;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class AccountInfoMenu:State
    {
        public override void ShowMenu()
        {
            using var context = new BankDbContext();
            var std = context.Users.Include(u => u.BankAccounts)
                .FirstOrDefault(u => _context.User.Id == u.Id)?.BankAccounts;
            if (std != null)
                foreach (var account in std)
                {
                    Console.WriteLine($"Account:\t {account.Account}");
                    Console.WriteLine($"Type:\t\t {account.Type}");
                    Console.WriteLine($"Balance:\t {account.Balance}");
                }

            Console.ReadKey();
            _context.TransitionTo(new AccountMenu());
        }
    }
}
