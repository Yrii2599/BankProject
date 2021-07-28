using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using Xunit;
using BankProject.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankProject.Tests
{
    public class AutorizationTests
    {
        [Fact]
        public void IsUserCreated()
        {
            var controller = Autorizer.CreateAccount("Bob", "Bob25");
            var std = Autorizer.TryGetUser(controller.Login,controller.Password);

                Assert.Equal("Bob", std.Login);
                Assert.Equal("Bob25", std.Password);
                Assert.Equal(std.GetType(), controller.GetType());


        }

        [Fact]
        public void TryGetUserFromDB()
        {
            var controller = Autorizer.TryGetUser("Bob", "Bob25");


            Assert.Equal("Bob", controller.Login);
            Assert.Equal("Bob25", controller.Password);
        }
    }
}
