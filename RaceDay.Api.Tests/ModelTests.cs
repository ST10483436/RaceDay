using RaceDay.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RaceDay.Api.Tests
{// start of the namespace
    public class ModelTests
    {// start of class
        [Fact]
        public void User_DefaultRole_IsEmpty()
        {
            var u = new User();
            Assert.Equal("", u.Role);
        }

        [Fact]
        public void EventEnrolment_DefaultStatus_IsRegistered()
        {
            var e = new EventEnrolment();
            Assert.Equal("Registered", e.Status);
        }

        [Fact]
        public void Event_DefaultEventName_IsEmpty()
        {
            var ev = new Event();
            Assert.Equal("", ev.EventName);
        }

        [Fact]
        public void Category_DefaultName_IsEmpty()
        {
            var c = new Category();
            Assert.Equal("", c.CategoryName);
        }


    }//end of class
}//end of the namespace
