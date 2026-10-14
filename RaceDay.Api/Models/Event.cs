namespace RaceDay.Api.Models
{// start of the namespace
    public class Event
    {//end of event class
        public int EventId { get; set; }
        public int OrganiserId { get; set; }
        public string EventName { get; set; } = "";
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = "";
        public decimal Distance { get; set; }
        public string EventType { get; set; } = "";


    }//end of event class
}//end of the namespace
