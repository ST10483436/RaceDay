using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{// start of the namespace

    /// <summary>
    /// Manages RaceDay events. Anyone can view events; only Organisers can create, update or delete.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
        private string? CurrentRole => HttpContext.Session.GetString("UserRole");

        /// <summary>
        /// Retrieves all events in the system (public).
        /// </summary>
        /// <response code="200">List of events returned.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var getter = new get_user_info();
            return Ok(getter.get_all_events());
        }

        /// <summary>
        /// Retrieves full details of a single event by ID (public).
        /// </summary>
        /// <param name="id">The event's unique ID.</param>
        /// <response code="200">Event found.</response>
        /// <response code="404">Event does not exist.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var getter = new get_user_info();
            var ev = getter.get_event_by_id(id);
            if (ev == null) return NotFound($"Event {id} not found.");
            return Ok(ev);
        }

        /// <summary>
        /// Creates a new event. Only the logged-in Organiser can create.
        /// </summary>
        /// <response code="201">Event created.</response>
        /// <response code="400">Validation error.</response>
        /// <response code="401">Not logged in.</response>
        /// <response code="403">User is not an Organiser.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Create(string name, string? description, DateTime date, string location, decimal distance, string eventType)
        {
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can create events.");
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
                return BadRequest("Name and Location are required.");
            if (eventType != "Run" && eventType != "Walk" && eventType != "Cycle")
                return BadRequest("EventType must be Run, Walk or Cycle.");

            var getter = new get_user_info();
            var org = getter.get_organiser_by_userId(CurrentUserId.Value);
            if (org == null) return BadRequest("Organiser profile not found.");

            var storer = new store_info();
            string result = storer.store_event(org.OrganiserId, name, description ?? "", date.ToString("yyyy-MM-dd HH:mm:ss"), location, distance, eventType);

            return StatusCode(201, new { message = result });
        }

        /// <summary>
        /// Updates an event owned by the logged-in Organiser.
        /// </summary>
        /// <response code="200">Event updated.</response>
        /// <response code="403">Not the owning Organiser.</response>
        /// <response code="404">Event not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, string name, string? description, DateTime date, string location, decimal distance, string eventType)
        {
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can update events.");

            var getter = new get_user_info();
            var ev = getter.get_event_by_id(id);
            if (ev == null) return NotFound();

            var org = getter.get_organiser_by_userId(CurrentUserId.Value);
            if (org == null || ev.OrganiserId != org.OrganiserId)
                return StatusCode(403, "You can only update your own events.");

            var updater = new update_user_info();
            string result = updater.update_event(id, name, description ?? "", date.ToString("yyyy-MM-dd HH:mm:ss"), location, distance, eventType);
            return Ok(new { message = result });
        }

        /// <summary>
        /// Deletes an event owned by the logged-in Organiser.
        /// </summary>
        /// <response code="204">Event deleted.</response>
        /// <response code="403">Not the owning Organiser.</response>
        /// <response code="404">Event not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (CurrentUserId == null) return Unauthorized("Not logged in.");
            if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can delete events.");

            var getter = new get_user_info();
            var ev = getter.get_event_by_id(id);
            if (ev == null) return NotFound();

            var org = getter.get_organiser_by_userId(CurrentUserId.Value);
            if (org == null || ev.OrganiserId != org.OrganiserId)
                return StatusCode(403, "You can only delete your own events.");

            var deleter = new delete_info();
            deleter.delete_event(id);
            return NoContent();
        }



    }//end of class
}//end of the namespace
