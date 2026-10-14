using System.Data.SqlClient;

namespace RaceDay.Api.database
{// start of the namespace
    public class store_info
    {// start of class
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        // Insert a new user and return the new UsersId
        public int store_user(string username, string hashedPassword, string email, string role)
        {
            int newId = 0;
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO Users (Username, Password, Email, Role) VALUES ('" + username + "', '" + hashedPassword + "', '" + email + "', '" + role + "'); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch { newId = 0; }
            return newId;
        }

        public string store_organiser(int usersId, string organiserName, string phoneNumber)
        {
            string message = "Failed to store organiser.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO Organisers (UsersId, OrganiserName, PhoneNumber) VALUES (" + usersId + ", '" + organiserName + "', '" + phoneNumber + "');";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Organiser saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string store_participant(int usersId, string firstName, string lastName, string dob, string phone)
        {
            string message = "Failed to store participant.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO Participants (UsersId, FirstName, LastName, DateOfBirth, PhoneNumber) VALUES (" + usersId + ", '" + firstName + "', '" + lastName + "', '" + dob + "', '" + phone + "');";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Participant saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string store_event(int organiserId, string eventName, string description, string eventDate, string location, decimal distance, string eventType)
        {
            string message = "Failed to store event.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO Events (OrganiserId, EventName, Description, EventDate, Location, Distance, EventType) VALUES (" + organiserId + ", '" + eventName + "', '" + (description ?? "") + "', '" + eventDate + "', '" + location + "', " + distance + ", '" + eventType + "');";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Event saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string store_category(int eventId, string categoryName, int? minAge, int? maxAge)
        {
            string message = "Failed to store category.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string min = minAge.HasValue ? minAge.Value.ToString() : "NULL";
                    string max = maxAge.HasValue ? maxAge.Value.ToString() : "NULL";
                    string query = "INSERT INTO Categories (EventId, CategoryName, MinAge, MaxAge) VALUES (" + eventId + ", '" + categoryName + "', " + min + ", " + max + ");";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Category saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string store_enrolment(int participantId, int eventId, int categoryId)
        {
            string message = "Failed to store enrolment.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO EventEnrolments (ParticipantId, EventId, CategoryId, Status) VALUES (" + participantId + ", " + eventId + ", " + categoryId + ", 'Registered');";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Enrolment saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string store_result(int eventId, int participantId, int categoryId, string finishTime, int position)
        {
            string message = "Failed to store result.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "INSERT INTO Results (EventId, ParticipantId, CategoryId, FinishTime, Position) VALUES (" + eventId + ", " + participantId + ", " + categoryId + ", '" + finishTime + "', " + position + ");";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    message = "Result saved.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

    }//end of class
}//end of the namespace
