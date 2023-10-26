using Prueba_XPD.Models;
using System.Data.SqlClient;
using System.Data;
using Prueba_XPD.Repositorio.Contrato;

namespace Prueba_XPD.Repositorio.Implementacion
{
    public class BoletosRepository : IGenericRepository<Boleto>
    {
        private readonly string _cadenaSQL = "";
        public BoletosRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Boleto>> GetAll()
        {
            List<Boleto> boletoList = new List<Boleto>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        boletoList.Add(new Boleto
                        {
                            Id_Boleto = Convert.ToInt32(dr["Id_Genero"]),
                            Precio = Convert.ToInt32(dr["Titulo"])
                        });
                    }
                }
            }
            return boletoList;
        }

        public async Task<bool> Post(Boleto modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("IngresarBoleto", conexion);
                cmd.Parameters.AddWithValue("Precio", modelo.Precio);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }

        public async Task<bool> Update(Boleto modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ModificarBoleto", conexion);
                cmd.Parameters.AddWithValue("Id_Boleto", modelo.Id_Boleto);
                cmd.Parameters.AddWithValue("Precio", modelo.Precio);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }
    }
}
