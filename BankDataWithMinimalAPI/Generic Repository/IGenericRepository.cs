namespace BankDataWithMinimalAPI.Generic_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task<T> Update(T obj);
        Task<T> Delete(object id);
    }
}
