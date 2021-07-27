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
    class PersonInfo
    {
        public static void ChangeInfo(User user)
        {
            PersonalinfoInterface info1 = new PersonalinfoInterface();
           
           
           using var context= new BankDbContext();
           var std = context.Users.Include(u=>u.PersonalInfo)
                                        .FirstOrDefault(u=>user.Id==u.Id);
           if (std!=null)
           {
               info1.InsertInfoData(std.PersonalInfo);
               context.SaveChanges();
           }
        }

        public static void ShowInfo(User user)
        {
            using (var context= new BankDbContext())
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
