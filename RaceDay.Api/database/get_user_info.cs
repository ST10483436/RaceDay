using RaceDay.Api.Models;
using System.Data.SqlClient;

namespace RaceDay.Api.database
{
    public class get_user_info
    {
        private string connection_string = @"Data source=(localdb)\api_guide; database=RACEDAY; Integrated Security=True;";

        public User? get_user_by_email(string email)
        {
            User? user = null;
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Users WHERE Email='" + email + "';";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
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
            }
            catch { }
            return user;
        }

        public Organiser? get_organiser_by_userId(int usersId)
        {
            Organiser? o = null;
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Organisers WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        o = new Organiser
                        {
                            OrganiserId = (int)reader["OrganiserId"],
                            UsersId = (int)reader["UsersId"],
                            OrganiserName = reader["OrganiserName"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!
                        };
                    }
                }
            }
            catch { }
            return o;
        }

        public Participant? get_participant_by_userId(int usersId)
        {
            Participant? p = null;
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Participants WHERE UsersId=" + usersId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        p = new Participant
                        {
                            ParticipantId = (int)reader["ParticipantId"],
                            UsersId = (int)reader["UsersId"],
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            PhoneNumber = reader["PhoneNumber"].ToString()!
                        };
                    }
                }
            }
            catch { }
            return p;
        }

        public List<Event> get_all_events()
        {
            List<Event> list = new List<Event>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Events;";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Event
                        {
                            EventId = (int)reader["EventId"],
                            OrganiserId = (int)reader["OrganiserId"],
                            EventName = reader["EventName"].ToString()!,
                            Description = reader["Description"]?.ToString(),
                            EventDate = (DateTime)reader["EventDate"],
                            Location = reader["Location"].ToString()!,
                            Distance = (decimal)reader["Distance"],
                            EventType = reader["EventType"].ToString()!
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        public Event? get_event_by_id(int id)
        {
            Event? ev = null;
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Events WHERE EventId=" + id + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
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
                    }
                }
            }
            catch { }
            return ev;
        }

        public List<Category> get_categories_for_event(int eventId)
        {
            List<Category> list = new List<Category>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Categories WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Category
                        {
                            CategoryId = (int)reader["CategoryId"],
                            EventId = (int)reader["EventId"],
                            CategoryName = reader["CategoryName"].ToString()!,
                            MinAge = reader["MinAge"] == DBNull.Value ? null : (int?)reader["MinAge"],
                            MaxAge = reader["MaxAge"] == DBNull.Value ? null : (int?)reader["MaxAge"]
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        public List<EventEnrolment> get_enrolments_by_event(int eventId)
        {
            List<EventEnrolment> list = new List<EventEnrolment>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM EventEnrolments WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new EventEnrolment
                        {
                            EnrolmentId = (int)reader["EnrolmentId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            EventId = (int)reader["EventId"],
                            CategoryId = (int)reader["CategoryId"],
                            Status = reader["Status"].ToString()!
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        public List<EventEnrolment> get_enrolments_by_participant(int participantId)
        {
            List<EventEnrolment> list = new List<EventEnrolment>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM EventEnrolments WHERE ParticipantId=" + participantId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new EventEnrolment
                        {
                            EnrolmentId = (int)reader["EnrolmentId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            EventId = (int)reader["EventId"],
                            CategoryId = (int)reader["CategoryId"],
                            Status = reader["Status"].ToString()!
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        public List<Result> get_results_by_event(int eventId)
        {
            List<Result> list = new List<Result>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Results WHERE EventId=" + eventId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Result
                        {
                            ResultId = (int)reader["ResultId"],
                            EventId = (int)reader["EventId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            CategoryId = (int)reader["CategoryId"],
                            FinishTime = reader["FinishTime"].ToString()!,
                            Position = (int)reader["Position"]
                        });
                    }
                }
            }
            catch { }
            return list;
        }

        public List<Result> get_results_by_participant(int participantId)
        {
            List<Result> list = new List<Result>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection_string))
                {
                    connect.Open();
                    string query = "SELECT * FROM Results WHERE ParticipantId=" + participantId + ";";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new Result
                        {
                            ResultId = (int)reader["ResultId"],
                            EventId = (int)reader["EventId"],
                            ParticipantId = (int)reader["ParticipantId"],
                            CategoryId = (int)reader["CategoryId"],
                            FinishTime = reader["FinishTime"].ToString()!,
                            Position = (int)reader["Position"]
                        });
                    }
                }
            }
            catch { }
            return list;
        }

    }
}
