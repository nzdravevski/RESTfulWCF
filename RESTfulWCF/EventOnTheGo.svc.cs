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
        public User PasswordCheck(string password, string username)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            User user = null;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read() && (reader["password"].ToString()) == password)
                {
                   user = new User
                   {
                       firstName = reader["firstName"].ToString(),
                       lastName = reader["lastName"].ToString(),
                       username = username,
                       password = password,
                       email = reader["email"].ToString(),
                       result = true
                   };
                }
                else
                {
                   user = new User
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

            return user;
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











    }
}
