using System;
using System.Linq;
using System.Security.Cryptography;
using BankProject.Helpers;
using BankProject.Models;

namespace BankProject
{
    class Program
    {
        static void Main(string[] args)
        {
           
            AutorizeInteface autorize = new AutorizeInteface();
            autorize.UserInterface();

        }
    }
}
