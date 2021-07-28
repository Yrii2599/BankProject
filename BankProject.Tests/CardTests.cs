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

        [Fact]
        public void TransferringMoneyFromOneCardToAnother()
        {
       
            BaseCard fromCardNumber = new BaseCard(){ BankAccount = { Account = "61327227081409325202339", Balance = 4500 },CardNumber = "8009632340604747", Pin = "7585" };
            var toCardNumber = new BaseCard {CardNumber = "5618695424030153", Pin = "0296" ,BankAccount ={Account = "34947183087748526930771", Balance = 51065} };
          
            int count = 5;
            using var context = new BankDbContext();
            fromCardNumber.CardToCard(toCardNumber, count);

            Assert.Equal(expected: fromCardNumber.Balance() - 5, actual: fromCardNumber.Balance());
            Assert.Equal(expected: toCardNumber.Balance() + 5, actual: toCardNumber.Balance());

        }

        [Fact]
        public void IsGetUserFromDb()
        {
            var card = new BaseCard() { CardNumber = "8009632340604747", Pin = "7585", BankAccount = new BaseAccount() { Account = "61327227081409325202339", Balance = 5000 } };
           
          var res=  BaseCard.GetCardByNumber(cardNumber: card.CardNumber);
            
            Assert.Equal(expected: card.CardNumber,actual: res.CardNumber);
            Assert.Equal(expected: card.Pin,actual: res.Pin);

        }
        [Fact]
        public void IsUserOwner()
        {
            List<BaseAccount> listBankAccounts= new List<BaseAccount>();
            List<BaseCard> listCards= new List<BaseCard>();
            
            BaseAccount account = new BaseAccount() {Account = "61327227081409325202339", Balance = 5000};
            listBankAccounts.Add(item: account);
            var card = new BaseCard() { CardNumber = "8009632340604747", Pin = "7585", BankAccount = account };
            listCards.Add(item: card);
            User user = new User() {BankAccounts = listBankAccounts,Login = "as", Password = "1"};

           var res= BaseCard.IsUserOwner(user: user, cardNumber: card.CardNumber);
         
           Assert.Equal(expected: true, actual: res );



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
