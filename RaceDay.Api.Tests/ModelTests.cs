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
        {// START OF User_DefaultRole_IsEmpty Method
            var u = new User();
            Assert.Equal("", u.Role);
        }// end of User_DefaultRole_IsEmpty method

        [Fact]
        public void EventEnrolment_DefaultStatus_IsRegistered()
        {// start of EventEnrolment_DefaultStatus_IsRegistered method
            var e = new EventEnrolment();
            Assert.Equal("Registered", e.Status);
        }//end of EventEnrolment_DefaultStatus_IsRegistered method

        [Fact]
        public void Event_DefaultEventName_IsEmpty()
        {// start of Event_DefaultEventName_IsEmpty method
            var ev = new Event();
            Assert.Equal("", ev.EventName);
        }//end of Event_DefaultEventName_IsEmpty method

        [Fact]
        public void Category_DefaultName_IsEmpty()
        {// start of Category_DefaultName_IsEmpty method
            var c = new Category();
            Assert.Equal("", c.CategoryName);
        }//end of Category_DefaultName_IsEmpty method


    }//end of class
}//end of the namespace
