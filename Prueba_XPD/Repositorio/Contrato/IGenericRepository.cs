namespace Prueba_XPD.Repositorio.Contrato
{
    public interface IGenericRepository<T> where T: class
    {
        Task<List<T>> GetAll();
        Task<bool> Post(T modelo);
        Task<bool> Update(T modelo);

    }
}
