using System;
using BankProject.Abstraction;

namespace BankProject.States
{
    class CardMenu:State
    {
        public override void ShowMenu()
        {
            Console.WriteLine($"1\t Open card for one of my account");
            Console.WriteLine($"2\t Send money by card");
            Console.WriteLine($"3\t Go to main Menu");
            Console.WriteLine($"\nPlease choose the options");
            int choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    Console.WriteLine("Enter the bank account for which you want to create a card.");
                    var account = Console.ReadLine();
                    BaseCard.AddToDB(_context.User, account);
                   ShowMenu();
                    break;
                case 2:
                    Console.WriteLine("Which card to transfer money to");
                    var toCardNumber = Console.ReadLine();
                    Console.WriteLine("From card to transfer money to");
                    var fromCardNumber = Console.ReadLine();
                    Console.WriteLine("Input count");
                    if (Int32.TryParse(Console.ReadLine(), out var count) && BaseCard.IsUserOwner(_context.User, fromCardNumber))
                    {
                        if (BaseCard.IsCardValid(toCardNumber) && BaseCard.IsCardValid(fromCardNumber))
                        {
                            BaseCard.GetCardByNumber(fromCardNumber)
                                .CardToCard(BaseCard.GetCardByNumber(toCardNumber), count);
                        }
                        else
                        {
                            Console.WriteLine($"\nOne of input card in not valid\n");
                        }
                    }

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
    }
}
