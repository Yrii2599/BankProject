using System;
using System.Collections.Generic;
using System.Text;

namespace BankProject.ConsoleInterfaces
{
    class AddMoneyMenu
    {
        public static void Menu(out string userAccount, out int count)
        {
            Console.WriteLine($"Input account number for transfer");
            userAccount = Console.ReadLine();
            Console.WriteLine($"Input count");
            count = Convert.ToInt32(Console.ReadLine());
        }
    }
}
