namespace RaceDay.Api.Models
{// start of the namespace
    public class User
    {// start of user class
        public int UsersId { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }//end of user class
}//end of the namespace
