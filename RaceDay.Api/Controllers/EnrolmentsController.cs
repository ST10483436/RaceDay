using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{// start of the namespace
   
    // Event enrolment operations for Participants and Organisers.
   
    [Route("api")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {// start of EnrolmentController class
        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        private string? CurrentRole => HttpContext.Session.GetString("UserRole");

       
        // Enrols the logged-in Participant into an event under a chosen category.
       
        /// <param name="eventId">The event to enrol in.</param>
        /// <param name="categoryId">The selected category.</param>
        /// <response code="201">Enrolment recorded.</response>
        /// <response code="403">User is not a Participant.</response>
        /// <response code="404">Event or category not found.</response>
        /// <response code="409">Already enrolled.</response>
        [HttpPost("events/{eventId}/enrolments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Enrol(int eventId, int categoryId)
        {//Start of enrol method
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Participant") return StatusCode(403, "Only Participants can enrol.");

            var getter = new get_user_info();
            var p = getter.get_participant_by_userId(CurrentUserId.Value);
            if (p == null) return BadRequest("Participant profile not found.");

            var ev = getter.get_event_by_id(eventId);
            if (ev == null) return NotFound($"Event {eventId} not found.");

            var cats = getter.get_categories_for_event(eventId);
            if (!cats.Any(c => c.CategoryId == categoryId))
                return NotFound("Category not found for this event.");

            var existing = getter.get_enrolments_by_participant(p.ParticipantId);
            if (existing.Any(e => e.EventId == eventId))
                return Conflict("Already enrolled in this event.");

            var storer = new store_info();
            string result = storer.store_enrolment(p.ParticipantId, eventId, categoryId);
            return StatusCode(201, new { message = result });
        }//end of enrol method

        
        // Retrieves all enrolments for a specific event (Organiser only).
     
        /// <response code="200">List of enrolments.</response>
        /// <response code="403">Not the owning Organiser.</response>
        [HttpGet("events/{eventId}/enrolments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetForEvent(int eventId)
        {//start of GetForEvent method
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can view event enrolments.");

            var getter = new get_user_info();
            var ev = getter.get_event_by_id(eventId);
            if (ev == null) return NotFound();

            var org = getter.get_organiser_by_userId(CurrentUserId.Value);
            if (org == null || ev.OrganiserId != org.OrganiserId)
                return StatusCode(403, "You can only view enrolments for your own events.");

            return Ok(getter.get_enrolments_by_event(eventId));
        }//end of GetForEvent method

       
        // Retrieves the logged-in Participant's own enrolment history.
       
        /// <response code="200">List of enrolments.</response>
        /// <response code="403">User is not a Participant.</response>
        [HttpGet("users/me/enrolments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult MyEnrolments()
        {// start of MyEnrolments method
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Participant") return StatusCode(403, "Only Participants can view their enrolments.");

            var getter = new get_user_info();
            var p = getter.get_participant_by_userId(CurrentUserId.Value);
            if (p == null) return NotFound();

            return Ok(getter.get_enrolments_by_participant(p.ParticipantId));
        }//end of Myenrolments methods


    }//end of class
}//end of the namespace
