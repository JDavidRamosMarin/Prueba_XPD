using Prueba_XPD.Models;
using Prueba_XPD.Repositorio.Contrato;
using System.Data.SqlClient;
using System.Data;

namespace Prueba_XPD.Repositorio.Implementacion
{
    public class VentasRepository : IGenericRepository<Ventas>
    {
        private readonly string _cadenaSQL = "";
        public VentasRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Ventas>> GetAll()
        {
            List<Ventas> ventasList = new List<Ventas>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("PeliculasActivasGenero", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        ventasList.Add(new Ventas
                        {
                            Id_Venta = Convert.ToInt32(dr["Id_venta"]),
                            refPeliculas = new Peliculas()
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
                            },
                            FechaVenta = dr["Titulo"].ToString(),
                            FechaPresentacion = dr["Titulo"].ToString(),         
                            Asiento = dr["Director"].ToString(),
                            Sala = Convert.ToInt32(dr["estatus"]),
                            Promocion = Convert.ToInt32(dr["estatus"]),
                        });
                    }
                }
            }
            return ventasList;
        }

        public async Task<bool> Post(Ventas modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("InsertarVentas", conexion);
                cmd.Parameters.AddWithValue("Id_Pelicula", modelo.refPeliculas.Id_Pelicula);
                cmd.Parameters.AddWithValue("FechaVenta", modelo.FechaVenta);
                cmd.Parameters.AddWithValue("FechaPresentacion", modelo.FechaPresentacion);
                cmd.Parameters.AddWithValue("Asiento", modelo.Asiento);
                cmd.Parameters.AddWithValue("Sala", modelo.Sala);
                cmd.Parameters.AddWithValue("Promocion", modelo.Promocion);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }

        public async Task<bool> Update(Ventas modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("InsertarVentas", conexion);
                cmd.Parameters.AddWithValue("Id_Venta", modelo.Id_Venta);
                cmd.Parameters.AddWithValue("Id_Pelicula", modelo.refPeliculas.Id_Pelicula);
                cmd.Parameters.AddWithValue("FechaVenta", modelo.FechaVenta);
                cmd.Parameters.AddWithValue("FechaPresentacion", modelo.FechaPresentacion);
                cmd.Parameters.AddWithValue("Asiento", modelo.Asiento);
                cmd.Parameters.AddWithValue("Sala", modelo.Sala);
                cmd.Parameters.AddWithValue("Promocion", modelo.Promocion);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0) return true;
                else return false;
            }
        }
    }
}
