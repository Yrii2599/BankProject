using System;
using System.Linq;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class BaseCard : Card
    {
        public int Balance()
        {
            return BankAccount.Balance;
        }

        public BaseCard()
        {
            CardNumber = GenerateCardNumber();
            Pin = GeneratePinForCard();
        }
        /// <summary>
        /// Money transfer from one card to another
        /// </summary>
        /// <param name="card">Bank account</param>
        /// <param name="count">Transfer volume</param>
        public void CardToCard(BaseCard card, int count)
        {
            this.BankAccount.SendMoney(card.BankAccount.Account,count);
        }
        /// <summary>
        /// Retrieving a custom map from a database
        /// </summary>
        /// <param name="cardNumber">the card number is transmitted to search for a card in the database</param>
        /// <returns>the map found in the database</returns>
        public static BaseCard GetCardByNumber(string cardNumber)
        {
            using var context = new BankDbContext();
            return context.Cards.FirstOrDefault(u => u.CardNumber == cardNumber);
        }
        /// <summary>
        /// Checks if the user is the owner of the card
        /// </summary>
        /// <param name="user">The user of which is checked whether the card number contains</param>
        /// <param name="cardNumber">number of the card checked for belonging to the user</param>
        /// <returns>true if the user owns the card number</returns>
        public static bool IsUserOwner(User user, string cardNumber)
        {
            return GetCardByNumber(cardNumber)?.BankAccount.User.Id == user.Id;
        }

        private string GenerateCardNumber()
        {
            Random _random = new Random();
            int[] numbers = new int[16];
            string cardNumber = "";
            double number = 0;
            double number1 = 0.0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {

                numbers[i] = _random.Next(0, 10);
                cardNumber += numbers[i];
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    numbers[i] *= 2;
                }

                if (numbers[i] > 9)
                {
                    numbers[i] -= 9;
                }

                number += numbers[i];
            }

            if (number % 10 != 0)
            {
                number1 = (int)(number) % 10;
                numbers[^1] = 10 - (int)number1;
            }

            cardNumber += numbers[^1];
            if (!IsCardValid(cardNumber))
            {
                return GenerateCardNumber();
            }
            return cardNumber;

        }

        private string GeneratePinForCard()
        {
            Random _random = new Random();
            int[] numbers = new int[4];
            string pin = "";
            for (int i = 0; i < numbers.Length; i++)
            {

                numbers[i] = _random.Next(0, 10);
                pin += numbers[i];
            }

            return pin;
        }

        /// <summary>
        /// checks the correctness of the card insertion
        /// </summary>
        /// <param name="cardNumber">the card that is being checked</param>
        /// <returns>true if the map is correct</returns>
        public static bool IsCardValid(string cardNumber)
        {

            int[] numbers = new int[16];
            int number = 0;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                numbers[i] = Convert.ToInt32(cardNumber[i]) - '0';
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    numbers[i] *= 2;
                }

                if (numbers[i] > 9)
                {
                    numbers[i] -= 9;
                }

                number += numbers[i];
            }

            number += numbers[^1];
            if (number % 10 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// create a card in the database for a specific user with a link to a bank account
        /// </summary>
        /// <param name="user">user who owns the account</param>
        /// <param name="account">account that is verified for affiliation</param>
        public static void AddToDB(User user, string account)
        {
            using var context = new BankDbContext();
            var res = context.BankAccounts.Include(u => u.Cards)
                .FirstOrDefault(u => u.User.Id == user.Id && u.Account == account);
            if (res != null)
            {
                if (res.Cards.Any())
                {
                    Console.WriteLine($"Card for this account Has already exist");
                }
                else
                {
                    var a = new BaseCard
                    {
                        BankAccount = res
                    };

                    res.Cards.Add(a);
                    context.SaveChanges();
                    Console.WriteLine($"\nCard successfully add");
                    Console.WriteLine($"card number {a.CardNumber}");
                    Console.WriteLine($"card pin {a.Pin}\n");

                }


            }
            else
            {
                Console.WriteLine($"You can only create cards for your own accounts");
            }
        }
    }
}
