using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;


namespace RaceDay.Api.Controllers
{// start of the namespace

    /// <summary>
    /// Handles user registration, login and logout for Organisers and Participants.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {// start of class
        
        // Registers a new user account and creates their role-specific profile.
        // <param name="fullName">The user's full name.</param>
        // <param name="email">The user's email (used as login).</param>
        // <param name="password">The user's password (minimum 8 characters).</param>
        // <param name="role">Either "Organiser" or "Participant".</param>
        // <param name="phoneNumber">Optional phone number.</param>
        // <param name="dateOfBirth">Required only for Participants.</param>
        // <response code="201">User registered successfully.</response>
        // <response code="400">Invalid input or role.</response>
        // <response code="409">Email already registered.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register(string fullName, string email, string password, string role,
                                       string? phoneNumber, DateTime? dateOfBirth)
        {// start of IActionResult Register method
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
                return BadRequest("Email, Password and Role are required.");

            if (role != "Organiser" && role != "Participant")
                return BadRequest("Role must be Organiser or Participant.");

            var getter = new get_user_info();
            if (getter.get_user_by_email(email) != null)
                return Conflict("Email already registered.");

            string hashed = BCrypt.Net.BCrypt.HashPassword(password);

            var storer = new store_info();
            int newUserId = storer.store_user(email.Split('@')[0], hashed, email, role);
            if (newUserId == 0) return StatusCode(500, "Failed to save user.");

            if (role == "Organiser")
            {//start of  if statement
                storer.store_organiser(newUserId, fullName, phoneNumber ?? "");
            }//end of if statement
            else
            {// start of else method
                var parts = (fullName ?? "").Split(' ', 2);
                string fn = parts[0];
                string ln = parts.Length > 1 ? parts[1] : "";
                string dob = (dateOfBirth ?? DateTime.Now.AddYears(-20)).ToString("yyyy-MM-dd");
                storer.store_participant(newUserId, fn, ln, dob, phoneNumber ?? "");
            }//end of else statement

            // return  statement

            return StatusCode(201, new { message = "Registered.", userId = newUserId, role = role });
        }//end of method 

        
        // Authenticates a user and starts a session storing their UserId and Role.
      
        /// <param name="email">Registered email address.</param>
        /// <param name="password">The user's password (plain text — will be hashed and compared).</param>
        /// <response code="200">Login successful, session created.</response>
        /// <response code="401">Invalid email or password.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login(string email, string password)
        {// start of login method 
            var getter = new get_user_info();
            var user = getter.get_user_by_email(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return Unauthorized("Invalid email or password.");

            HttpContext.Session.SetInt32("UserId", user.UsersId);
            HttpContext.Session.SetString("UserRole", user.Role);

            // return statement
            return Ok(new { message = "Logged in.", userId = user.UsersId, role = user.Role });
        }//end of login method

       
        // Ends the current user's session.
       
        /// <response code="200">Session cleared.</response>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {// start of Logout method
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out." });
        }//end of logout Method


    }//end of class
}//end of the namespace