using System;
using System.Linq;
using BankProject.Abstraction;
using BankProject.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BankProject.States
{
    class ShowPersonalInfo: State
    {
        public override void ShowMenu()
        {
            using var context = new BankDbContext();
            var std = context.Users.Include(u => u.PersonalInfo)
                .FirstOrDefault(u => _context.User.Id == u.Id);
            if (std != null)
            {
                Console.WriteLine($"Name:\t\t {std.PersonalInfo.Name}");
                Console.WriteLine($"Surname:\t {std.PersonalInfo.Surname}");
                Console.WriteLine($"Secret word:\t {std.PersonalInfo.SecretWord}");
            }
            Console.ReadKey();
            _context.TransitionTo(new InfoMenu());
        }
    }
}
