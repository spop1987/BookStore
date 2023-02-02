namespace BookStore.Service.Generic
{
    public interface IGenericRepository<T> where T : class
    {
         Task<T> GetById(long id);
         Task<IEnumerable<T>> GetAll();
         Task Delete(long id);
    }
}