using System;
using System.Linq;
using BankProject.Abstraction;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject.States
{
    class InfoMenu:State
    {
        public override void ShowMenu()
        {
            Console.WriteLine($"1\t Change personal info");
            Console.WriteLine($"2\t Show personal info");
            Console.WriteLine($"3\t Go to main Menu");
            Console.WriteLine($"\nPlease choose the options");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    _context.TransitionTo(new ChangeInfoMenu());
                    ShowMenu();
                    break;
                case 2:
                    _context.TransitionTo(new ShowPersonalInfo());
                    ShowMenu();
                    break;
                case 3:
                    _context.TransitionTo(new MainMenu());
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }
       

        public static void ShowInfo(User user)
        {
            using (var context = new BankDbContext())
            {
                var std = context.Users.Include(u => u.PersonalInfo)
                    .FirstOrDefault(u => user.Id == u.Id);
                Console.WriteLine($"Name:\t\t {std.PersonalInfo.Name}");
                Console.WriteLine($"Surname:\t {std.PersonalInfo.Surname}");
                Console.WriteLine($"Secret word:\t {std.PersonalInfo.SecretWord}");
            }
        }
    }
}
