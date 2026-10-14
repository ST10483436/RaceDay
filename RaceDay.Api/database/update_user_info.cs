using System.Data.SqlClient;

namespace RaceDay.Api.database
{
    public class update_user_info
    {
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        public string update_user_email(int usersId, string newEmail)
        {
            string message = "Failed to update user.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "UPDATE Users SET Email='" + newEmail + "', Username='" + newEmail.Split('@')[0] + "' WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "User updated.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string update_organiser(int usersId, string name, string phone)
        {
            string message = "Failed to update organiser.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "UPDATE Organisers SET OrganiserName='" + name + "', PhoneNumber='" + phone + "' WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Organiser updated.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string update_participant(int usersId, string fullName, string phone)
        {
            string message = "Failed to update participant.";
            try
            {
                var parts = fullName.Split(' ', 2);
                string fn = parts[0];
                string ln = parts.Length > 1 ? parts[1] : "";
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "UPDATE Participants SET FirstName='" + fn + "', LastName='" + ln + "', PhoneNumber='" + phone + "' WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Participant updated.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string update_event(int eventId, string name, string description, string date, string location, decimal distance, string eventType)
        {
            string message = "Failed to update event.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = @"UPDATE Events SET EventName='" + name + "', Description='" + (description ?? "") + "', EventDate='" + date + "', Location='" + location + "', Distance=" + distance + ", EventType='" + eventType + "' WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Event updated.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string update_category(int categoryId, string name, int? minAge, int? maxAge)
        {
            string message = "Failed to update category.";
            try
            {
                string min = minAge.HasValue ? minAge.Value.ToString() : "NULL";
                string max = maxAge.HasValue ? maxAge.Value.ToString() : "NULL";
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "UPDATE Categories SET CategoryName='" + name + "', MinAge=" + min + ", MaxAge=" + max + " WHERE CategoryId=" + categoryId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Category updated.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

    }
}
