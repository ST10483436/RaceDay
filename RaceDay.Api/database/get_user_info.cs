using RaceDay.Api.Models;
using System.Data.SqlClient;

namespace RaceDay.Api.database
{// start of the namespace
    public class get_user_info
    {// start of class 
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        public User? get_user_by_email(string email)
        {// start of user method
            User? user = null;
            try
            {// try statements
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// start of using
                    connect.Open();
                    string query = "SELECT * FROM Users WHERE Email='" + email + "';";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {// if statements
                        user = new User
                        {
                            UsersId = (int)reader["UsersId"],
                            Username = reader["Username"].ToString()!,
                            Password = reader["Password"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            Role = reader["Role"].ToString()!
                        };
                    }
                }
            }//end of if statements
            catch { }// catch staements
            // return statements
            return user;
        }//end of method

        public Organiser? get_organiser_by_userId(int usersId)
        {// start of oganiser method
            Organiser? o = null;
            try
            {// start of try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {//using
                    connect.Open();
                    string query = "SELECT * FROM Organisers WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {// if statements
                        o = new Organiser
                        {
                            OrganiserId = (int)reader["OrganiserId"],
                            UsersId = (int)reader["UsersId"],
                            OrganiserName = reader["OrganiserName"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!
                        };
                    }
                }
            }//end of if statement
            catch { }// catch statement
            return o;
        }//end of method

        public Participant? get_participant_by_userId(int usersId)
        {// start of method
            Participant? p = null;
            try
            {// start of try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Participants WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {// if statement
                        p = new Participant
                        {
                            ParticipantId = (int)reader["ParticipantId"],
                            UsersId = (int)reader["UsersId"],
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            PhoneNumber = reader["PhoneNumber"].ToString()!
                        };
                    }//end of if
                }//end of using sql connection
            }//end of try
            catch { }// catch statement
            return p;
        }//end of method

        public List<Event> get_all_events()
        {// start 
            List<Event> list = new List<Event>();
            try
            {// try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Events;";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// while statement
                        list.Add(new Event
                        {// listing 
                            EventId = (int)reader["EventId"],
                            OrganiserId = (int)reader["OrganiserId"],
                            EventName = reader["EventName"].ToString()!,
                            Description = reader["Description"]?.ToString(),
                            EventDate = (DateTime)reader["EventDate"],
                            Location = reader["Location"].ToString()!,
                            Distance = (decimal)reader["Distance"],
                            EventType = reader["EventType"].ToString()!
                        });
                    }//end of while 
                }//end of using
            }//end of try
            catch { }
            return list;
        }

        public Event? get_event_by_id(int id)
        {// start of method
            Event? ev = null;
            try
            {// try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// using sql connection
                    connect.Open();
                    string query = "SELECT * FROM Events WHERE EventId=" + id + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {// if statements
                        ev = new Event
                        {
                            EventId = (int)reader["EventId"],
                            OrganiserId = (int)reader["OrganiserId"],
                            EventName = reader["EventName"].ToString()!,
                            Description = reader["Description"]?.ToString(),
                            EventDate = (DateTime)reader["EventDate"],
                            Location = reader["Location"].ToString()!,
                            Distance = (decimal)reader["Distance"],
                            EventType = reader["EventType"].ToString()!
                        };
                    }//end of if statements
                }//end of using sql connection
            }//end of try
            catch { }// catch statement
            return ev;
        }//end of method

        public List<Category> get_categories_for_event(int eventId)
        {// start ofmethod
            List<Category> list = new List<Category>();
            try
            {// try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// using
                    connect.Open();
                    string query = "SELECT * FROM Categories WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// start of while statement
                        list.Add(new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            EventId = (int)reader["EventId"],
                            CategoryName = reader["CategoryName"].ToString()!,
                            MinAge = reader["MinAge"] == DBNull.Value ? null : (int?)reader["MinAge"],
                            MaxAge = reader["MaxAge"] == DBNull.Value ? null : (int?)reader["MaxAge"]
                        });
                    }//end of while statement
                }//end of using sql connection 
            }//end of try 
            catch { }//end of catch
            return list;
        }//end of method

        public List<EventEnrolment> get_enrolments_by_event(int eventId)
        {// start of method
            List<EventEnrolment> list = new List<EventEnrolment>();
            try
            {//start of try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// yusing sql connection
                    connect.Open();
                    string query = "SELECT * FROM EventEnrolments WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// start of while statements
                        list.Add(new EventEnrolment
                        {
                            EnrolmentId = (int)reader["EnrolmentId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            EventId = (int)reader["EventId"],
                            CategoryId = (int)reader["CategoryId"],
                            Status = reader["Status"].ToString()!
                        });
                    }//end of while statement
                }//end using sql connection
            }//end of try
            catch { }// catch statement
            return list;
        }//end of method 

        public List<EventEnrolment> get_enrolments_by_participant(int participantId)
        {// start of method
            List<EventEnrolment> list = new List<EventEnrolment>();
            try
            {// start of try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// start of using
                    connect.Open();
                    string query = "SELECT * FROM EventEnrolments WHERE ParticipantId=" + participantId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// start of while 
                        list.Add(new EventEnrolment
                        {// list
                            EnrolmentId = (int)reader["EnrolmentId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            EventId = (int)reader["EventId"],
                            CategoryId = (int)reader["CategoryId"],
                            Status = reader["Status"].ToString()!
                        });// list
                    }//end of while
                }// end of using sql
            }//end of try
            catch { }// end of catch
            return list;
        }//end of method

        public List<Result> get_results_by_event(int eventId)
        {// start of method
            List<Result> list = new List<Result>();
            try
            {// try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {//using
                    connect.Open();
                    string query = "SELECT * FROM Results WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// start of while 
                        list.Add(new Result
                        {// list
                            ResultId = (int)reader["ResultId"],
                            EventId = (int)reader["EventId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            CategoryId = (int)reader["CategoryId"],
                            FinishTime = reader["FinishTime"].ToString()!,
                            Position = (int)reader["Position"]
                        });//list
                    }//end of while
                }//end using sql
            }//end  try
            catch { }// catch
            return list;
        }//end of method

        public List<Result> get_results_by_participant(int participantId)
        {// start of method
            List<Result> list = new List<Result>();
            try
            {//try
                using (SqlConnection connect = new SqlConnection(connection_string))
                {// using sql
                    connect.Open();
                    string query = "SELECT * FROM Results WHERE ParticipantId=" + participantId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {// start of while
                        list.Add(new Result
                        {// list
                            ResultId = (int)reader["ResultId"],
                            EventId = (int)reader["EventId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            CategoryId = (int)reader["CategoryId"],
                            FinishTime = reader["FinishTime"].ToString()!,
                            Position = (int)reader["Position"]
                        });
                    }//end of while
                }//end of using sql
            }//end of try
            catch { }// catch statement
            return list;
        }//end of method

    }//nd of class
}//end of the namespace
