using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RaceDay.Api.Tests
{// start of the namespace
    public class RoleValidationTests
    {// start of rolevalidation class
        private bool IsValidRole(string role) => role == "Organiser" || role == "Participant";
        private bool IsValidEventType(string type) => type == "Run" || type == "Walk" || type == "Cycle";

        [Theory]
        [InlineData("Organiser")]
        [InlineData("Participant")]
        public void ValidRoles_AreAccepted(string role)
        {// start of ValidRoles_AreAccepted method
            Assert.True(IsValidRole(role));
        }//end of ValidRoles_AreAccepted method 

        [Theory]
        [InlineData("Admin")]
        [InlineData("")]
        [InlineData("organiser")]
        public void InvalidRoles_AreRejected(string role)
        {// start of InvalidRoles_AreRejected method
            Assert.False(IsValidRole(role));
        }// end of InvalidRoles_AreRejected method

        [Theory]
        [InlineData("Run")]
        [InlineData("Walk")]
        [InlineData("Cycle")]
        public void ValidEventTypes_AreAccepted(string type)
        {// start of method
            Assert.True(IsValidEventType(type));
        }//end of method

        [Theory]
        [InlineData("Swim")]
        [InlineData("run")]
        [InlineData("")]
        public void InvalidEventTypes_AreRejected(string type)
        {// start of method
            Assert.False(IsValidEventType(type));
        }//end of method 

    }//end of class
}//end of the namespace
