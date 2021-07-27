using System;
using System.Linq;
using BankProject.Abstraction;
using BankProject.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BankProject.States
{
    class ChangeInfoMenu:State
    {
        public override void ShowMenu()
        {
            Console.WriteLine($"Please input your personal info");
            Console.WriteLine($"Input your name");
            var name = Console.ReadLine();
            Console.WriteLine($"Input your surname");
            var surname = Console.ReadLine();
            Console.WriteLine($"Input your secret words for recovery password");
           var secretWord = Console.ReadLine();
            using var context = new BankDbContext();
            var std = context.Users.Include(u => u.PersonalInfo)
                .FirstOrDefault(u => _context.User.Id == u.Id);
            if (std != null)
            {
                std.PersonalInfo.Name=name;
                std.PersonalInfo.Surname=surname;
                std.PersonalInfo.SecretWord=secretWord;
                context.SaveChanges();
                Console.WriteLine("Info was changed successful");
                Console.ReadKey();
                _context.TransitionTo(new InfoMenu());
            }
            else
            {
                Console.WriteLine($"Info not found");
                Console.ReadKey();
                _context.TransitionTo(new AutorizeMenu());
            }
        }
    }
}
