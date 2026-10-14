using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RaceDay.Api.Tests
{
    public class RoleValidationTests
    {
        private bool IsValidRole(string role) => role == "Organiser" || role == "Participant";
        private bool IsValidEventType(string type) => type == "Run" || type == "Walk" || type == "Cycle";

        [Theory]
        [InlineData("Organiser")]
        [InlineData("Participant")]
        public void ValidRoles_AreAccepted(string role)
        {
            Assert.True(IsValidRole(role));
        }

        [Theory]
        [InlineData("Admin")]
        [InlineData("")]
        [InlineData("organiser")]
        public void InvalidRoles_AreRejected(string role)
        {
            Assert.False(IsValidRole(role));
        }

        [Theory]
        [InlineData("Run")]
        [InlineData("Walk")]
        [InlineData("Cycle")]
        public void ValidEventTypes_AreAccepted(string type)
        {
            Assert.True(IsValidEventType(type));
        }

        [Theory]
        [InlineData("Swim")]
        [InlineData("run")]
        [InlineData("")]
        public void InvalidEventTypes_AreRejected(string type)
        {
            Assert.False(IsValidEventType(type));
        }

    }
}
