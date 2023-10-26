using Prueba_XPD.Models;
using Prueba_XPD.Repositorio.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace Prueba_XPD.Repositorio.Implementacion
{
    public class PeliculasRepository: IGenericRepository<Peliculas>, IPeliculas<Peliculas>
    {
        private readonly string _cadenaSQL = "";
        public PeliculasRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Peliculas>> GetAll()
        {
            List<Peliculas> peliculasList = new List<Peliculas>();
            using( var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("PeliculasActivasGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        peliculasList.Add(new Peliculas
                        {
                            Id_Pelicula = Convert.ToInt32(dr["Id_Pelicula"]),
                            Titulo = dr["Titulo"].ToString(),
                            refGenero = new Genero()
                            {
                                Id_Genero = Convert.ToInt32(dr["Id_Genero"]),
                                Nombre = dr["Nombre"].ToString()
                            },
                            Director = dr["Director"].ToString(),
                            FechaEstreno = dr["FechaEstreno"].ToString(),
                            idioma = dr["idioma"].ToString(),
                            Estatus = Convert.ToInt32(dr["estatus"])
                        });
                    }
                }
            }
            return peliculasList;
        }

        public int GetCountActiveMovies()
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                int conteo = 0;
                SqlCommand cmd = new SqlCommand("PeliculasActivas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter returnValue = cmd.Parameters.Add("return_value", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;

                conexion.Open();
                Convert.ToInt32(cmd.ExecuteNonQuery());
                conteo = (int)returnValue.Value;
                conexion.Close();
                return conteo;   
            }
        }

        public async Task<List<Peliculas>> GetMoviesPrice()
        {
            List<Peliculas> peliculasList = new List<Peliculas>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("PeliculasActivasGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        peliculasList.Add(new Peliculas
                        {
                            Id_Pelicula = Convert.ToInt32(dr["Id_Pelicula"]),
                            Titulo = dr["Titulo"].ToString(),
                            refGenero = new Genero()
                            {
                                Id_Genero = Convert.ToInt32(dr["Id_Genero"]),
                                Nombre = dr["Nombre"].ToString()
                            },
                            Director = dr["Director"].ToString(),
                            FechaEstreno = dr["FechaEstreno"].ToString(),
                            idioma = dr["idioma"].ToString(),
                            Estatus = Convert.ToInt32(dr["estatus"]),
                            refBoleto = new Boleto()
                            {
                                Id_Boleto = Convert.ToInt32(dr["Id_Boleto"]),
                                Precio = Convert.ToInt32(dr["Precio"])
                            }
                        });
                    }
                }
            }
            return peliculasList;
        }

        public async Task<bool> Post(Peliculas modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("IngresarPeliculas", conexion);
                cmd.Parameters.AddWithValue("Titulo", modelo.Titulo);
                cmd.Parameters.AddWithValue("Id_Genero", modelo.refGenero.Id_Genero);
                cmd.Parameters.AddWithValue("Director", modelo.Director);
                cmd.Parameters.AddWithValue("FechaEstreno", modelo.FechaEstreno);
                cmd.Parameters.AddWithValue("idioma", modelo.idioma);
                cmd.Parameters.AddWithValue("estatus", modelo.Estatus);
                cmd.Parameters.AddWithValue("Id_Boleto", modelo.refBoleto.Id_Boleto);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }

        public async Task<bool> Update(Peliculas modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ModificarPeliculas", conexion);
                cmd.Parameters.AddWithValue("Id_Pelicula", modelo.Id_Pelicula);
                cmd.Parameters.AddWithValue("Titulo", modelo.Titulo);
                cmd.Parameters.AddWithValue("Id_Genero", modelo.refGenero.Id_Genero);
                cmd.Parameters.AddWithValue("Director", modelo.Director);
                cmd.Parameters.AddWithValue("FechaEstreno", modelo.FechaEstreno);
                cmd.Parameters.AddWithValue("idioma", modelo.idioma);
                cmd.Parameters.AddWithValue("estatus", modelo.Estatus);
                cmd.Parameters.AddWithValue("Id_Boleto", modelo.refBoleto.Id_Boleto);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }
    }
}
