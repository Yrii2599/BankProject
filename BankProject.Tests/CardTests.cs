using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BankProject.Tests
{
  public  class CardTests
    {
        [Fact]
        public void IsCreatedBaseCard()
        {
            var card = new BaseCard();
            using var context = new BankDbContext();
            var std = context.Cards.FirstOrDefault(predicate: u=>u.CardNumber==card.CardNumber);
            if (std != null)
            {
                Assert.Equal(expected: std.GetType(), actual: card.GetType());
                Assert.Equal(expected: std.CardNumber, actual: card.CardNumber);
                Assert.Equal(expected: std.Pin, actual: card.Pin);
            }

            Assert.Equal(expected: 4, actual: card.Pin.Length);
            Assert.Equal(expected: 16, actual: card.CardNumber.Length);


        }

        [Theory]
        [InlineData("Allaa", "1",150,500)]
        [InlineData("Vasia", "123",300,1000)]
        [InlineData("Zhenia", "2536",256,400)]
        public void TransferringMoneyFromOneCardToAnother(string name, string password, int count, int balance)
        {
            var user = new User(){BankAccounts = new List<BaseAccount>(), Login = name, Password = password};
            var card=new BaseCard();
            var card1=new BaseCard();
            BaseAccount accountFrom = new BaseAccount();
            BaseAccount accountTo = new BaseAccount();
            accountFrom.Balance = balance;
            accountFrom.Cards = new List<BaseCard>();
            accountFrom.User = user;
            accountTo.Balance = balance;
            accountTo.Cards = new List<BaseCard>();
            accountTo.User = user;
            card.BankAccount = accountFrom;
            card1.BankAccount = accountTo;
            user.BankAccounts.Add(accountFrom);
            user.BankAccounts.Add(accountTo);
            accountFrom.Cards.Add(card);
            accountTo.Cards.Add(card1);

            using (var context = new BankDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            BaseCard fromCardNumber;
            BaseCard toCardNumber;
            using (var context = new BankDbContext())
            {
                fromCardNumber = context.Cards.Include(u=>u.BankAccount).FirstOrDefault(u=>u.CardNumber==card.CardNumber);
                toCardNumber = context.Cards.Include(u => u.BankAccount).FirstOrDefault(u=>u.CardNumber == card1.CardNumber);
            }

            if (fromCardNumber != null&& toCardNumber != null)
            {
                fromCardNumber.CardToCard(toCardNumber, count);

                
            }
            using (var context = new BankDbContext())
            {
                fromCardNumber = context.Cards.Include(u => u.BankAccount).FirstOrDefault(u => u.CardNumber == card.CardNumber);
                toCardNumber = context.Cards.Include(u => u.BankAccount).FirstOrDefault(u => u.CardNumber == card1.CardNumber);
            }

            if (fromCardNumber != null && toCardNumber != null)
            {
                Assert.Equal(expected: balance- count, actual: fromCardNumber.Balance());
                Assert.Equal(expected: balance + count, actual: toCardNumber.Balance());
            }

            using (var context = new BankDbContext())
            {
                context.Remove(user);
                context.SaveChanges();
            }



        }
        [Theory]
        [InlineData("Allaa","1")]
        [InlineData("Vasia","123")]
        [InlineData("Zhenia","2536")]
        public void IsUserOwner(string name, string password)
        {
            List<BaseAccount> listBankAccounts= new List<BaseAccount>();
            List<BaseCard> listCards= new List<BaseCard>();
            var user= new User();
            BaseAccount account = new BaseAccount() {Cards = listCards, User =user };
            listBankAccounts.Add(item: account);
            var card = new BaseCard() { BankAccount = account};
            listCards.Add(item: card);
            user.BankAccounts = listBankAccounts;
            user.Login = name;
            user.Password = password;

            using (var context = new BankDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            User userFromDb;
            using (var context = new BankDbContext())
            {
                userFromDb = context.Users.Include(u=>u.BankAccounts)
                    .ThenInclude(u=>u.Cards)
                    .FirstOrDefault(u => u.Login == name);
            }
            var res = BaseCard.IsUserOwner(user: userFromDb, cardNumber: card.CardNumber);
            Assert.True( res );
            Assert.NotNull(userFromDb);
            Assert.NotNull(userFromDb.BankAccounts);
            Assert.True(userFromDb.BankAccounts.Any(u=>u.Account==account.Account));
            var baseAccount = userFromDb.BankAccounts.FirstOrDefault(u => u.Account == account.Account);
            Assert.NotNull(baseAccount);
            Assert.NotNull(baseAccount.Cards);
            Assert.NotEmpty(baseAccount.Cards);
            Assert.True(baseAccount.Cards.Any(u=>u.CardNumber==card.CardNumber));
            using (var context = new BankDbContext())
            {
                context.Users.Remove(userFromDb);
                context.SaveChanges();
            }



        }
        [Fact]
        public void IsCardGenerateCorrect()
        {
            var card = new BaseCard() { CardNumber = "8009632340604747", Pin = "7585" };
            
           var result= BaseCard.IsCardValid(card.CardNumber);
            Assert.Equal(true,result );
        }
        [Fact]
        public void IsPinOfCardGenerateCorrect()
        {
            var card = new BaseCard() { CardNumber = "8009632340604747", Pin = BaseCard.GeneratePinForCard() };
            Assert.Equal(4, card.Pin.Length);
        }

        [Fact]
        public void WasCardAdedToDb()
        {
            List<BaseAccount> listBankAccounts = new List<BaseAccount>();
            List<BaseCard> listCards = new List<BaseCard>();
            BaseAccount account = new BaseAccount() { Account = "61327227081409325202339", Balance = 5000 };
            listBankAccounts.Add(item: account);
            var card = new BaseCard() { CardNumber = "1111111111111111", Pin = "7585", BankAccount = account };
            listCards.Add(item: card);
            User user = new User() { BankAccounts = listBankAccounts, Login = "as", Password = "1" };

           BaseCard.AddToDB(user, account.Account);
           using var context = new BankDbContext();
           var res = context.Cards
               .FirstOrDefault(u => u.CardNumber == card.CardNumber && u.BankAccount.Account==account.Account);
           
           if (res != null)
           {
               Assert.Equal(card.CardNumber, res.CardNumber);
               Assert.Equal(card.Balance(), res.Balance());
           }
        }
    }
}
