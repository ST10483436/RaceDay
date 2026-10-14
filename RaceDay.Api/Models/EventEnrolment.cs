namespace RaceDay.Api.Models
{// start of the namespace
    public class EventEnrolment
    {// start of eventEnrolment class
        public int EnrolmentId { get; set; }
        public int ParticipantId { get; set; }
        public int EventId { get; set; }
        public int CategoryId { get; set; }
        public string Status { get; set; } = "Registered";
    }// end of eventenrolment class
}// end of the namespace
