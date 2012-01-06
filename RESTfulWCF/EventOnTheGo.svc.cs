using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;

namespace RESTfulWCF
{
    public class EventOnTheGo : IEventOnTheGo
    {
        public User Login(string username, User user)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            User temp = null;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read() && (reader["password"].ToString()) == user.password) 
                {
                   temp = new User
                   {
                       firstName = reader["firstName"].ToString(),
                       lastName = reader["lastName"].ToString(),
                       username = username,
                       password = user.password,
                       email = reader["email"].ToString(),
                       result = true
                   };
                }
                else //if database query returns nothing or passwords doesnt match
                {
                   temp = new User
                   {
                       firstName = null,
                       lastName = null,
                       username = null,
                       password = null,
                       email = null,
                       result = false
                   };
                }
                reader.Close();
            }
            catch (Exception) { }
            finally
            {
                connection.Close();
            }

            return temp;
        }

        public Result Registration(User user)
        {
            Result temp = null;
            
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Users (firstName, lastName, username, password, email) VALUES (@firstName, @lastName, @username, @password, @email)";
            command.Parameters.AddWithValue("@firstName", user.firstName);
            command.Parameters.AddWithValue("@lastName", user.lastName);
            command.Parameters.AddWithValue("@username", user.username);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@email", user.email);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception) 
            {
                temp = new Result
                {
                    result = false
                };
                return temp;
            }
            finally
            {
                connection.Close();
            }

            temp = new Result
            {
                result = true
            };

            return temp;
        }

        public Result UpdateUserProfile(User user)
        {
            Result temp = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Users SET firstName = @firstName, lastName = @lastName, password = @password, email = @email WHERE username = @username";
            command.Parameters.AddWithValue("@firstName", user.firstName);
            command.Parameters.AddWithValue("@lastName", user.lastName);
            command.Parameters.AddWithValue("@username", user.username);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@email", user.email);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                temp = new Result
                {
                    result = false
                };
                return temp;
            }
            finally
            {
                connection.Close();
            }

            temp = new Result
            {
                result = true
            };

            return temp;
    
        }

        public Result AddEvent(Event myEvent)
        {
            Result temp = null;

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Events (userID, locationName, locationLat, locationLon, eventName, eventGoal, dateFrom, dateTo) VALUES (@userId, @venueName, @venueLat, @venueLon, @eventName, @eventGoal, @dateFrom, @dateTo)";
            command.Parameters.AddWithValue("@userId", myEvent.userId);
            command.Parameters.AddWithValue("@venueName", myEvent.venueName);
            command.Parameters.AddWithValue("@venueLat", Convert.ToDouble(myEvent.venueLat));
            command.Parameters.AddWithValue("@venueLon", Convert.ToDouble(myEvent.venueLon));
            command.Parameters.AddWithValue("@eventName", myEvent.eventName);
            command.Parameters.AddWithValue("@eventGoal", myEvent.eventGoal);
            command.Parameters.AddWithValue("@dateFrom", myEvent.dateFrom);
            command.Parameters.AddWithValue("@dateTo", myEvent.dateTo);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message + e.Source + e.Data);
                temp = new Result
                {
                    result = false
                };
                return temp;
            }
            finally
            {
                connection.Close();
            }

            temp = new Result
            {
                result = true
            };

            return temp;
        }

        public Event[] GetAllEvents()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Events";

            List<Event> allEvents = new List<Event>();
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    allEvents.Add( //userID, locationName, locationLat, locationLon, eventName, eventGoal, dateFrom, dateTo
                        new Event
                        {
                            userId = reader[1].ToString(),
                            venueName = reader[2].ToString(),
                            venueLat = Convert.ToDouble(reader[3]),
                            venueLon = Convert.ToDouble(reader[4]),
                            eventName = reader[5].ToString(),
                            eventGoal = reader[6].ToString(),
                            dateFrom = Convert.ToDateTime(reader[7]),
                            dateTo = Convert.ToDateTime(reader[8])
                        });
                }
                reader.Close();
            }
            catch (Exception e) 
            {

            }
            finally
            {
                connection.Close();
            }

            return allEvents.ToArray();
        }













    }
}
