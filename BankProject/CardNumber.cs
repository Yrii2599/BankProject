using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
    class CardNumber : Card
    {
        public CardNumber(BaseAccount account)
        {
            Balance = account.Balance;
            CardNumber = GenerateCardNumber();
            Pin = GeneratePinForCard();
        }

        static Random _random = new Random();

        public static string GenerateCardNumber()
        {

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
                number1 = (int) (number) % 10;
                numbers[^1] = 10 - (int) number1;
            }

            cardNumber += numbers[^1];
            Console.WriteLine(cardNumber);
            return cardNumber;

        }

        public static string GeneratePinForCard()
        {
            int[] numbers = new int[4];
            string pin = "";
            for (int i = 0; i < numbers.Length; i++)
            {

                numbers[i] = _random.Next(0, 10);
                pin += numbers[i];
            }

            return pin;
        }


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

        public static void AddToDB(int id)
        {
            Console.WriteLine("Enter the bank account for which you want to create a card.");
            var account = Console.ReadLine();

            using var context = new BankDbContext();
            var res = context.BankAccounts.Include(u => u.Cards)
                .FirstOrDefault(u => u.Id == id && u.Account == account);
            if (res != null)
            {
                if (res.Cards.Any(u => u.Balance > 0))
                {
                    Console.WriteLine($"Card for this account Has already exist");
                }
                else
                {
                    var a = new CardNumber(res);
                    if (IsCardValid(a.CardNumber))
                    {
                        res.Cards.Add(a);
                        context.SaveChanges();
                    }
                }


            }
            else
            {
                Console.WriteLine($"You can only create cards for your own accounts");
            }
        }
    }
}
