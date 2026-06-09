using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Repository.Abstraction
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        // Queries
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);

        // this method is used to get an entity by its unique identifier, which is typically a Guid in many applications. It returns a Task that resolves to the entity if found, or null if not found.
        Task<T?> GetByIdAsync(Guid id);

        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

        // Commands
        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
