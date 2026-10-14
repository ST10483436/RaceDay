using System.Data.SqlClient;

namespace RaceDay.Api.database
{
    public class delete_info
    {
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        public string delete_event(int eventId)
        {
            string message = "Failed to delete event.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "DELETE FROM Events WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Event deleted.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }

        public string delete_category(int categoryId)
        {
            string message = "Failed to delete category.";
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "DELETE FROM Categories WHERE CategoryId=" + categoryId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Category deleted.";
                }
            }
            catch (Exception error) { message = error.Message; }
            return message;
        }
    }
}
