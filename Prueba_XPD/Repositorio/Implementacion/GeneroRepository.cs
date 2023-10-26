using Prueba_XPD.Models;
using Prueba_XPD.Repositorio.Contrato;
using System.Data.SqlClient;
using System.Data;

namespace Prueba_XPD.Repositorio.Implementacion
{
    public class GeneroRepository: IGenericRepository<Genero>
    {
        private readonly string _cadenaSQL = "";
        public GeneroRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Genero>> GetAll()
        {
            List<Genero> generoList = new List<Genero>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        generoList.Add(new Genero
                        {
                            Id_Genero = Convert.ToInt32(dr["Id_Genero"]),
                            Nombre = dr["Titulo"].ToString()
                        });
                    }
                }
            }
            return generoList;
        }

        public async Task<bool> Post(Genero modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("IngresarGenero", conexion);
                cmd.Parameters.AddWithValue("Nombre", modelo.Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }

        public async Task<bool> Update(Genero modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ModificarGenero", conexion);
                cmd.Parameters.AddWithValue("Id_Genero", modelo.Id_Genero);
                cmd.Parameters.AddWithValue("Nombre", modelo.Nombre);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }
    }
}
