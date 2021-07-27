using System;
using System.Collections.Generic;
using System.Text;

namespace BankProject.ConsoleInterfaces
{
    class SendMoneyInterface
    {
        public static  void SendInterface(out string toAccount, out int count, out string fromAccount)
        {
            Console.WriteLine($"Input account number for transfer");
            toAccount = Console.ReadLine();
            Console.WriteLine($"Input count");
            count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"From what number take a transaction");
            fromAccount = Console.ReadLine();
        }
    }
}
