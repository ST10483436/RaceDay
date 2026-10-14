namespace RaceDay.Api.Models
{// start of the namespace
    public class Participant
    {// start of participant class
        public int ParticipantId { get; set; }
        public int UsersId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = "";
    }//end of participant class
}//end of the namespace
