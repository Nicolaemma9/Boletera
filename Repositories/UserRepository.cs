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
                command.CommandText = "INSERT INTO [Usuario]" +
                    "VALUES (@Username, @Password, @Name," +
                    "@LastName, @Email)";
                //command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@UserName", userModel.UserName);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@LastName", userModel.LastName);
                command.Parameters.AddWithValue("@Email", userModel.Email);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
        //revisar para futuro cambio de inicio de sesion de username a email
        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool ValidUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [Usuario]" +
                    "where Username = @username and [Contrasena] = @password";
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
                command.CommandText = "DELETE FROM [Usuario] WHERE Id = @Id";
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
                command.CommandText = "SELECT * FROM [Usuario]" +
                    "WHERE Username = @username";
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
                command.CommandText = "UPDATE [Usuario] SET " +
                    "Username = @UserName, Nombre = @Name," +
                    "Apellido = @LastName, Email = @Email WHERE Id = @Id";
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
            using (var command = new SqlCommand("SELECT * FROM [Usuario]", connection))
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
        public bool EmailExiste(string email)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = "SELECT 1 FROM Usuario WHERE Email = @Email";
                command.Parameters.AddWithValue("@Email", email);

                return command.ExecuteScalar() != null;
            }
        }
    }
}
