using Boletera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [User]" +
                    "VALUES (@Id, @Username, @Password, @Name," +
                    "@LastName, @Email)";
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@UserName", userModel.UserName);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("LastName", userModel.LastName);
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool ValidUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User]" +
                    "where Userame = @username and [Password] = @password";
                command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;

                command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;
                ValidUser = command.ExecuteScalar() == null ? false : true;

            }
            return ValidUser;
        }

        public void Delete(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [User] WHERE id = @Id";
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        //esta funcion no es necesariamente util o necesaria para este trabajo, sin embargo
        //esto podria funcionar para nuestro trabajo

        //estas funciones deben de estar en IUserRepository, por lo que si se quiere lograr un  get "lastName" dene de estar tambien aca
        public UserModel GetByUername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [User]" +
                    "WHERE Userame = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            UserName = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Update(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET " +
                    "Userame = @UserName, Name = @Name," +
                    "LasteName = @LastName, Email = @Email WHERE id = @Id";
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@UserName", userModel.UserName);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@LastName", userModel.LastName);
                command.Parameters.AddWithValue("@Email", userModel.Email);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Id = reader[0].ToString(),
                            UserName = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString()
                        });
                    }
                }
            }

            return users;
        }

    }
}
