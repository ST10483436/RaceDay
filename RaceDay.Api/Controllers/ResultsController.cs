using Microsoft.AspNetCore.Mvc;
using RaceDay.Api.database;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{// start of the namespace

        /// <summary>
        /// Captures and retrieves finishing results for RaceDay events.
        /// </summary>
        [Route("api")]
        [ApiController]
        public class ResultsController : ControllerBase
        {// start of class
            private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
            private string? CurrentRole => HttpContext.Session.GetString("UserRole");

            /// <summary>
            /// Records a finishing time and position for a Participant (Organiser only).
            /// </summary>
            /// <response code="201">Result recorded.</response>
            /// <response code="403">Not the owning Organiser.</response>
            /// <response code="404">Event or participant not found.</response>
            /// <response code="409">Result already recorded.</response>
            [HttpPost("events/{eventId}/results")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status403Forbidden)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status409Conflict)]
            public IActionResult Create(int eventId, int participantId, int categoryId, string finishTime, int position)
            {
                if (CurrentUserId == null) return Unauthorized("Not logged in.");
                if (CurrentRole != "Organiser") return StatusCode(403, "Only Organisers can record results.");

                var getter = new get_user_info();
                var ev = getter.get_event_by_id(eventId);
                if (ev == null) return NotFound($"Event {eventId} not found.");

                var org = getter.get_organiser_by_userId(CurrentUserId.Value);
                if (org == null || ev.OrganiserId != org.OrganiserId)
                    return StatusCode(403, "You can only record results for your own events.");

                if (!TimeSpan.TryParse(finishTime, out _))
                    return BadRequest("FinishTime must be in HH:mm:ss format.");
                if (position <= 0) return BadRequest("Position must be positive.");

                var existing = getter.get_results_by_event(eventId);
                if (existing.Any(r => r.ParticipantId == participantId))
                    return Conflict("Result already recorded for this participant.");

                var storer = new store_info();
                string result = storer.store_result(eventId, participantId, categoryId, finishTime, position);
                return StatusCode(201, new { message = result });
            }

            /// <summary>
            /// Retrieves all results for a specific event (public).
            /// </summary>
            /// <response code="200">List of results.</response>
            /// <response code="404">Event not found.</response>
            [HttpGet("events/{eventId}/results")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public IActionResult GetForEvent(int eventId)
            {
                var getter = new get_user_info();
                if (getter.get_event_by_id(eventId) == null)
                    return NotFound($"Event {eventId} not found.");
                return Ok(getter.get_results_by_event(eventId));
            }

            /// <summary>
            /// Retrieves the logged-in Participant's own results history.
            /// </summary>
            /// <response code="200">List of results.</response>
            /// <response code="403">User is not a Participant.</response>
            [HttpGet("users/me/results")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status403Forbidden)]
            public IActionResult MyResults()
            {
                if (CurrentUserId == null) return Unauthorized("Not logged in.");
                if (CurrentRole != "Participant") return StatusCode(403, "Only Participants can view their results.");

                var getter = new get_user_info();
                var p = getter.get_participant_by_userId(CurrentUserId.Value);
                if (p == null) return NotFound();

                return Ok(getter.get_results_by_participant(p.ParticipantId));
            }

        }//end of class
    }//end of the namespace
