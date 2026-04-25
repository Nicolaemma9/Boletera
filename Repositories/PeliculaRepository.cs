using Boletera.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Repositories
{
    internal class PeliculaRepository : RepositoryBase, IPeliculaRepository
    {
        public void Add(PeliculaModel peliculaModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [Pelicula]" +
                    "VALUES (@Nombre, @Precio, @Sala," +
                    "@Idioma, @Subtitulos, @Horarios)";
                //command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@Nombre", peliculaModel.Nombre);
                command.Parameters.AddWithValue("@Precio", peliculaModel.Precio);
                command.Parameters.AddWithValue("@Sala", peliculaModel.Sala);
                command.Parameters.AddWithValue("@Idioma", peliculaModel.Idioma);
                command.Parameters.AddWithValue("@Subtitulos", peliculaModel.Subtitulos);
                command.Parameters.AddWithValue("@Horarios", peliculaModel.Horarios);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void Delete(PeliculaModel peliculaModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [Pelicula] WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", peliculaModel.Id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }



        public void Update(PeliculaModel peliculaModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [Pelicula] SET " +
                    "Nombre = @Nombre, Precio = @Precio," +
                    "Idioma = @Idioma, Subtitulos = @Subtitulos, Horarios = @Horarios WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", peliculaModel.Id);
                command.Parameters.AddWithValue("@Nombre", peliculaModel.Nombre);
                command.Parameters.AddWithValue("@Precio", peliculaModel.Precio);
                command.Parameters.AddWithValue("@Idioma", peliculaModel.Idioma);
                command.Parameters.AddWithValue("@Subtitulos", peliculaModel.Subtitulos);
                command.Parameters.AddWithValue("@Horarios", peliculaModel.Horarios);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public IEnumerable<PeliculaModel> GetAllPeliculas()
        {
            List<PeliculaModel> peliculas = new List<PeliculaModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM [Pelicula]", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        peliculas.Add(new PeliculaModel
                        {
                            Id = reader[0].ToString(),
                            Nombre = reader[1].ToString(),
                            Precio = string.Empty,
                            Sala = reader[3].ToString(),
                            Idioma = reader[4].ToString(),
                            Subtitulos = reader[5].ToString(),
                            Horarios = reader[6].ToString()
                        });
                    }
                }
            }
            return peliculas;
        }

    }
}

