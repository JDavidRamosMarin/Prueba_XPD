namespace Prueba_XPD.Repositorio.Contrato
{
    public interface IPeliculas<T> where T : class
    {
        int GetCountActiveMovies();
        Task<List<T>> GetMoviesPrice();
    }
}
