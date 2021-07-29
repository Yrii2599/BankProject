using System;
using BankProject.Abstraction;

namespace BankProject.States
{
    class AutorizeMenu : State
    {
        public override void ShowMenu()
        {
            int choose;
            Console.WriteLine($"\n1\t Create an account");
            Console.WriteLine($"2\t Log into account");
            Console.WriteLine($"0\t Exit");
            choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                {
                    Console.WriteLine("Input Login");
                    var login = Console.ReadLine();
                    Console.WriteLine("Input password");
                    var inputPassword = Console.ReadLine();
                    if (inputPassword != null && inputPassword.Length > 6)
                    {
                        Console.WriteLine($"Please confirm your password");
                        var confirmPassword = Console.ReadLine();
                        if (inputPassword == confirmPassword)
                        {



                            if (Autorizer.TryGetUser(login, inputPassword) != null)
                            {
                                Console.WriteLine($"This account is already exist, please create your own account");
                                ShowMenu();
                            }
                            else
                            {
                                var user = Autorizer.CreateAccount(login, inputPassword);
                                _context.User = user;
                                Console.WriteLine("Account was successful created");
                                _context.TransitionTo(new MainMenu());

                            }

                        }
                        else
                        {
                            Console.WriteLine($"Your password is wrong, please try again");
                            ShowMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Password must be more then 6 symbols ");
                        ShowMenu();
                    }

                    break;
                }
                case 2:
                {
                    Console.WriteLine("Input Login");
                    var login = Console.ReadLine();
                    Console.WriteLine("Input password");
                    var inputPassword = Console.ReadLine();
                    var inputUser = Autorizer.TryGetUser(login, inputPassword);
                    if (inputUser != null)
                    {
                        Console.WriteLine($"Welcome to your account ");
                        _context.User = inputUser;
                        _context.TransitionTo(new MainMenu());
                    }
                    else
                    {
                        Console.WriteLine($"Wrong login or password");
                        ShowMenu();
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
