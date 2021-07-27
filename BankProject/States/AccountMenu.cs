using System;
using BankProject.Abstraction;

namespace BankProject.States
{
    class AccountMenu: State
    {
        public override void ShowMenu()
        {
            
            Console.WriteLine($"1\t Open deposit account");
            Console.WriteLine($"2\t Open credit account");
            Console.WriteLine($"3\t Send money");
            Console.WriteLine($"4\t Add money");
            Console.WriteLine($"5\t Withdraw money");
            Console.WriteLine($"6\t View account information");
            Console.WriteLine($"7\t Go to main Menu");
            Console.WriteLine($"\nPlease choose the options");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    new DepositAccount().AddToDB(_context.User.Id);
                    ShowMenu();
                    break;
                case 2:
                    new CreditAccountcs().AddToDB(_context.User.Id);
                    ShowMenu();
                    break;
                case 3:
                    _context.TransitionTo(new SendMoneyToAccountMenu());
                    ShowMenu();
                    break;
                case 4:
                   _context.TransitionTo(new AddMoneyMenu());
                    ShowMenu();
                    break;
                case 5:
                    _context.TransitionTo(new WidthdrawMoneyMenu());
                    ShowMenu();
                    break;
                case 6:
                    _context.TransitionTo(new AccountInfoMenu());
                    ShowMenu();
                    break;
                case 7:
                    _context.TransitionTo(new MainMenu());
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }
    }
}
