using System.Data.SqlClient;

namespace RaceDay.Api.database
{// start of the namespace
    public class delete_info
    {// start of class
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        public string delete_event(int eventId)
        {// start of method
            string message = "Failed to delete event.";
            try
            {// try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// using
                    connect.Open();
                    string query = "DELETE FROM Events WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Event deleted.";
                }//end of using 
            }//end of try
            catch (Exception error) { message = error.Message; }//end of catch
            return message;
        }//end of method

        public string delete_category(int categoryId)
        {// start of method
            string message = "Failed to delete category.";
            try
            {// start of try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// using sql connection
                    connect.Open();
                    string query = "DELETE FROM Categories WHERE CategoryId=" + categoryId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) message = "Category deleted.";
                }//edn of using sql connection
            }//end of try
            catch (Exception error) { message = error.Message; }// catch
            return message;
        }//end of method
    }//end of the class
}//end of the namespace
