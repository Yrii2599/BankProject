using System;
using System.Linq;
using BankProject.Abstraction;
using BankProject.Helpers;

namespace BankProject.States
{
    class WidthdrawMoneyMenu: State
    {
        public override void ShowMenu()
        {
            Console.WriteLine($"Input account number for transfer");
           var accountNumber = Console.ReadLine();
            Console.WriteLine($"Input count");
           var count = Convert.ToInt32(Console.ReadLine());
           using (var context = new BankDbContext())
           {
               var result = context.BankAccounts
                   .FirstOrDefault(u => u.Account == accountNumber && u.User.Id == _context.User.Id);
               if (result != null)
               {
                   result.Balance -= count;
                   context.SaveChanges();
                   Console.WriteLine("Operation success");
                   Console.ReadKey();
                    _context.TransitionTo(new AccountMenu());
               }
               else
               {
                   Console.WriteLine($"You can only top up your accounts");
                   Console.ReadKey();
                   _context.TransitionTo(new AccountMenu());
                }
           }
        }
    }
}
