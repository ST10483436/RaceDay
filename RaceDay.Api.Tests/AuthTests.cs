using System.Data.SqlClient;
using System.Net;
using RaceDay.Api.database;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Api.Tests
{// start of the namespace

    // tests for register, login, logout, sessions and the profile
    [Collection("api")]
    public class AuthTests
    {//start of class

        [Fact]
        public void Hash_ReturnsDifferentValue_FromPlainText()
        {// start of Hash_ReturnsDifferentValue_FromPlainText method
            string plain = "Password123!";
            string hash = BCrypt.Net.BCrypt.HashPassword(plain);
            Assert.NotEqual(plain, hash);
            Assert.StartsWith("$2", hash);
        }//end of Hash_ReturnsDifferentValue_FromPlainText method

        [Fact]
        public void Verify_CorrectPassword_ReturnsTrue()
        {// start of Verify_CorrectPassword_ReturnsTrue method
            string plain = "Password123!";
            string hash = BCrypt.Net.BCrypt.HashPassword(plain);
            Assert.True(BCrypt.Net.BCrypt.Verify(plain, hash));
        }//end of Verify_CorrectPassword_ReturnsTrue method

        [Fact]
        public void Verify_WrongPassword_ReturnsFalse()
        {// start of verify_wrong password _return false method
            string hash = BCrypt.Net.BCrypt.HashPassword("CorrectPass1!");
            Assert.False(BCrypt.Net.BCrypt.Verify("WrongPass1!", hash));
        }//end of verify_wrong password _return false method

        [Fact]
        public void Hash_SamePasswordTwice_ReturnsDifferentHashes()
        {// start of Hash_SamePasswordTwice_ReturnsDifferentHashes method
            string h1 = BCrypt.Net.BCrypt.HashPassword("Password123!");
            string h2 = BCrypt.Net.BCrypt.HashPassword("Password123!");
            Assert.NotEqual(h1, h2);
        }//end of Hash_SamePasswordTwice_ReturnsDifferentHashes methods


    }//end of class
}//end of the namespace
