using System;
using System.Linq;
using BankProject.Abstraction;
using BankProject.Helpers;

namespace BankProject.States
{
    class SendMoneyToAccountMenu:State
    {
        public override void ShowMenu()
        {
            Console.WriteLine($"Input account number for transfer");
            var toAccount = Console.ReadLine();
            Console.WriteLine($"Input count");
          var   count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"From what number take a transaction");
           var  fromAccount = Console.ReadLine();
           using var context = new BankDbContext();
           var result = context.BankAccounts.FirstOrDefault(u => u.Account == fromAccount);
           if (result != null)
           {
               result.SendMoney(toAccount, count);
               context.SaveChanges();
               Console.WriteLine("Operation successful");
               Console.ReadKey();
               _context.TransitionTo(new AccountMenu());
           }
           else
           {
               Console.WriteLine("Operation failed");
               Console.ReadKey();
               _context.TransitionTo(new AccountMenu());
            }
        }
    }
}
