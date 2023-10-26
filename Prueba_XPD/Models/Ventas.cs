namespace Prueba_XPD.Models
{
    public class Ventas
    {
        public int Id_Venta { get; set; }
        public Peliculas refPeliculas { get; set; }
        public string FechaVenta { get; set; }
        public string FechaPresentacion { get; set; }
        public string Asiento { get; set; }
        public int Sala { get; set; }
        public int Promocion { get; set; }
    }
}
