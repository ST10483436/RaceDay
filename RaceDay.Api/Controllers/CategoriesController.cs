using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{// start of the namespace

    public class CategoriesController : ControllerBase
    {//start of categoriesController class

        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        private string? CurrentRole => HttpContext.Session.GetString("UserRole");

       
        // Retrieves all categories for a specific event (public).
        
        /// <param name="eventId">The event's ID.</param>
        /// <response code="200">List of categories.</response>
        /// <response code="404">Event not found.</response>
        [HttpGet("events/{eventId}/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetForEvent(int eventId)
        {//START FOR GETFOREVENT METHOD
            var getter = new get_user_info();
            if (getter.get_event_by_id(eventId) == null)
                return NotFound($"Event {eventId} not found.");
            return Ok(getter.get_categories_for_event(eventId));
        }// END FOR GETFOREVENT METHOD

        
        // Creates a new category under a specific event (Organiser only).
       
        /// <response code="201">Category created.</response>
        /// <response code="403">Not the owning Organiser.</response>
        /// <response code="404">Event not found.</response>
        [HttpPost("events/{eventId}/categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create(int eventId, string categoryName, int? minAge, int? maxAge)
        {// START OF CREATE METHOD
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can create categories.");

            var getter = new get_user_info();
            var ev = getter.get_event_by_id(eventId);
            if (ev == null) return NotFound($"Event {eventId} not found.");

            var org = getter.get_organiser_by_userId(CurrentUserId.Value);
            if (org == null || ev.OrganiserId != org.OrganiserId)
                return StatusCode(403, "You can only add categories to your own events.");

            var storer = new store_info();
            string result = storer.store_category(eventId, categoryName, minAge, maxAge);
            return StatusCode(201, new { message = result });
        }// END OF CREATE METHOD

        
        // Updates an existing category (Organiser only).
        
        /// <response code="200">Category updated.</response>
        /// <response code="403">Not the owning Organiser.</response>
        [HttpPut("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Update(int id, string categoryName, int? minAge, int? maxAge)
        {// START OF UPDATE METHOD
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can update categories.");

            var updater = new update_user_info();
            string result = updater.update_category(id, categoryName, minAge, maxAge);
            return Ok(new { message = result });
        }// END  OF UPDATE METHOD

        
        // Deletes a category (Organiser only).
        
        /// <response code="200">Category deleted.</response>
        /// <response code="403">Not the owning Organiser.</response>
        [HttpDelete("categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Delete(int id)
        {// START OF DELETE METHOD
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can delete categories.");

            var deleter = new delete_info();
            string result = deleter.delete_category(id);
            return Ok(new { message = result });
        }//END OF DELETE METHOD


    }//end of CategoriesController class
}//end of the namespace
