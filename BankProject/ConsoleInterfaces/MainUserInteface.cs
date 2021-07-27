using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;

namespace BankProject.ConsoleInterfaces
{
    class MainUserInteface
    {
        public void MainManu(User user)
        {
            Console.WriteLine($"\nPlease choose the options");
            Console.WriteLine($"1\t Change personal info");
            Console.WriteLine($"2\t Show personal info");
            Console.WriteLine($"3\t Open deposit account");
            Console.WriteLine($"4\t Open credit account");
            Console.WriteLine($"5\t Send money");
            Console.WriteLine($"6\t Add money");
            Console.WriteLine($"7\t Withdraw money");
            Console.WriteLine($"8\t View account information");
            Console.WriteLine($"9\t Open card for one of my account");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    PersonInfo.ChangeInfo(user);
                    MainManu(user);
                    break;
                case 2:
                    PersonInfo.ShowInfo(user);
                    MainManu(user);
                    break;
                case 3:
                   new DepositAccount().AddToDB(user.Id);
                    MainManu(user);
                    break;
                case 4:
                    new CreditAccountcs().AddToDB(user.Id);
                    MainManu(user);
                    break;
                case 5:
                    SendMoneySaveToDb.SendMoneyUpdateDb(user);
                    MainManu(user);
                    break;
                case 6:
                    MoneyOperationToDb.AddMoney(user);
                    MainManu(user);
                    break;
                case 7:
                   MoneyOperationToDb.WidthrawMoney(user);
                    MainManu(user);
                    break;
                case 8:
                  MoneyOperationToDb.AccountInfo(user);
                    MainManu(user);
                    break;
                case 9:
                   CardNumber.AddToDB(user.Id);
                    MainManu(user);
                    break;

            }

        }
    }
}
