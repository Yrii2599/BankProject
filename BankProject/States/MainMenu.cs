using System;
using BankProject.Abstraction;

namespace BankProject.States
{
    class MainMenu:State
    {
        public override void ShowMenu()
        {
            
            Console.WriteLine($"1\t Work with cards");
            Console.WriteLine($"2\t Work with accounts");
            Console.WriteLine($"3\t Work personal info");
            Console.WriteLine($"4\t Log out");
            Console.WriteLine($"\nPlease choose the options");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    _context.TransitionTo(new CardMenu());
                    break;
                case 2:
                _context.TransitionTo(new AccountMenu());
                    break;
                case 3:
                _context.TransitionTo(new InfoMenu());
                    break;
                case 4:
                    _context.User = null;
                   _context.TransitionTo(new AutorizeMenu());
                    break;
                default: 
                    ShowMenu();
                    break;
            }
        }
    }
}
