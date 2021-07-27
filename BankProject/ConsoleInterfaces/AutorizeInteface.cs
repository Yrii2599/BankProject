using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using BankProject.ConsoleInterfaces;
using BankProject.Models;

namespace BankProject
{
    class AutorizeInteface
    {
        public void UserInterface()
        {
            int choose;
            Console.WriteLine($"\n1\t Create an account");
            Console.WriteLine($"2\t Log into account");
            Console.WriteLine($"0\t Exit");
            choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1 :
                {
                    Console.WriteLine("Input Login");
                    var login = Console.ReadLine();
                    Console.WriteLine("Input password");
                    var inputPassword = Console.ReadLine();
                    Console.WriteLine($"Please confirm your password");
                    var confirmPassword = Console.ReadLine();
                    if (inputPassword==confirmPassword)
                    {
                        if (Autorize.TryGetUser(login, inputPassword)!=null)
                        {
                            Console.WriteLine($"This account is already exist, please create your own account");
                            UserInterface();
                        }
                        else
                        {
                            Autorize.CreateAccount(login, inputPassword);
                            Console.WriteLine("Account was successful created");
                            UserInterface();
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Your password is wrong, please try again");
                        UserInterface();
                    }
                   
                    break;
                }
                case 2:
                {
                    Console.WriteLine("Input Login");
                    var login = Console.ReadLine();
                    Console.WriteLine("Input password");
                    var inputPassword = Console.ReadLine();
                    var inputUser = Autorize.TryGetUser(login, inputPassword);
                    if (inputUser!=null)
                    {
                        Console.WriteLine($"Welcome to your account ");
                        MainUserInteface menu = new MainUserInteface();
                        menu.MainManu(inputUser);
                    }
                    else
                    {
                        Console.WriteLine($"Wrong login or password");
                    }
                    break;
                }
                case 0:
                {
                    Environment.Exit(0);
                    break;
                }
            }
          
        }
    }
}
