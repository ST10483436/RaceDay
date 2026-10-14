using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{// start of the namespace

    /// <summary>
    /// View and update the currently logged-in user's profile.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {// Start of usersController class
        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        private string? CurrentRole => HttpContext.Session.GetString("UserRole");

       
        // Returns the profile of the currently logged-in user.
        
        /// <response code="200">Profile returned.</response>
        /// <response code="401">Not logged in.</response>
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Me()
        {// start of Me method

            // if statements
            if (CurrentUserId == null) return Unauthorized("Not logged in.");

            var getter = new get_user_info();

            if (CurrentRole == "Organiser")
            {// start 
                var org = getter.get_organiser_by_userId(CurrentUserId.Value);
                return Ok(new
                {
                    userId = CurrentUserId,
                    role = CurrentRole,
                    fullName = org?.OrganiserName,
                    phoneNumber = org?.PhoneNumber
                });
            }//end 

            var p = getter.get_participant_by_userId(CurrentUserId.Value);
            return Ok(new
            {// start of return statement
                userId = CurrentUserId,
                role = CurrentRole,
                fullName = p != null ? p.FirstName + " " + p.LastName : "",
                phoneNumber = p?.PhoneNumber
            });//end of return statement
        }//end of Me method

        
        // Updates the currently logged-in user's profile.
        
        /// <param name="email">New email address.</param>
        /// <param name="fullName">New full name.</param>
        /// <param name="phoneNumber">New phone number.</param>
        /// <response code="200">Profile updated.</response>
        /// <response code="401">Not logged in.</response>
        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Update(string email, string fullName, string phoneNumber)
        {// start of update method
            if (CurrentUserId == null) return Unauthorized("Not logged in.");

            var updater = new update_user_info();
            updater.update_user_email(CurrentUserId.Value, email);

            if (CurrentRole == "Organiser")
                updater.update_organiser(CurrentUserId.Value, fullName, phoneNumber);
            else
                updater.update_participant(CurrentUserId.Value, fullName, phoneNumber);

            return Ok(new { message = "Profile updated." });
        }//end of update method


    }//end of class
}//end of the namespace
