namespace RaceDay.Api.Models
{// start of the namespace
    public class Result
    {// start of result class
        public int ResultId { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public int CategoryId { get; set; }
        public string FinishTime { get; set; } = "";
        public int Position { get; set; }


    }//end of result class
}//end of the namespace
