using PracticeBlog.Data.Models;

namespace PracticeBlog.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T item);
        Task Update(T item);
        Task Delete(T item);
        User GetByLogin(string login);
    }
}
