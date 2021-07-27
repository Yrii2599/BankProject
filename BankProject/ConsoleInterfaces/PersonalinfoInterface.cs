using System;
using System.Collections.Generic;
using System.Text;
using BankProject.Models;

namespace BankProject.ConsoleInterfaces
{
    class PersonalinfoInterface 
    {
        public PersonalInfo InsertInfoData(PersonalInfo userInfo)
        {
            Console.WriteLine($"Please input your personal info");
            Console.WriteLine($"Input your name");
            userInfo.Name=Console.ReadLine();
            Console.WriteLine($"Input your surname");
            userInfo.Surname = Console.ReadLine();
            Console.WriteLine($"Input your secret words for recovery password");
            userInfo.SecretWord = Console.ReadLine();
            return userInfo;

        }
    }
}
