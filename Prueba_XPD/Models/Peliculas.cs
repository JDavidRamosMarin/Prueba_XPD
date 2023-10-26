namespace Prueba_XPD.Models
{
    public class Peliculas
    {
        public int Id_Pelicula { get; set; }
        public string Titulo { get; set; }
        public Genero refGenero { get; set; }
        public string Director { get; set; }
        public string FechaEstreno { get; set; }
        public string idioma { get; set; }
        public int Estatus { get; set; }
        public Boleto refBoleto { get; set; }
    }
}
