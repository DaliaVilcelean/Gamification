namespace Gamification.Repositories
{
    public interface IBaseRepository<T>
    {
      //  Task<T> Create(T t);

        Task<T> Update(T t);
        Task Delete(T t);
        Task<List<T>> FindAll();
        Task<T> FindById(Guid id);

    }
}
