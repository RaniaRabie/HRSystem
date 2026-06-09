using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Repository.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        // This method retrieves all entities of type T from the database. If a predicate is provided, it filters the results based on the given condition. If no predicate is provided, it returns all entities.    
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            try
            {
                if (predicate == null) 
                    return await _context.Set<T>().ToListAsync();
                else
                    return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        
        /* ----------------------------------------------------------------------------------------*/


        // This method retrieves an entity of type T from the database based on its unique identifier (id). It uses the FindAsync method to locate the entity with the specified id. If the entity is found, it returns it; otherwise, it returns null.
        public async Task<T?> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return null;
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                    return null;
                else
                    return entity;  
            }
            catch (Exception)
            {
                throw;
            }
        }

        /* ----------------------------------------------------------------------------------------*/
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        // This method adds a new entity of type T to the database. It uses the AddAsync method to add the entity to the context and then calls SaveChangesAsync to persist the changes to the database. If any exceptions occur during this process, they are caught and rethrown.
        public async Task AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /* ----------------------------------------------------------------------------------------*/

        // This method updates an existing entity of type T in the database. It uses the Update method to mark the entity as modified in the context and then calls SaveChanges to persist the changes to the database.
        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /* ----------------------------------------------------------------------------------------*/

        // This method deletes an existing entity of type T from the database. It uses the Remove method to mark the entity for deletion in the context and then calls SaveChanges to persist the changes to the database.
        public void Delete(T entity)
        {
            try
            {   if (entity is SoftDeletableEntity softDelete)
                {
                    // Soft delete => flag it as deleted
                    softDelete.IsDeleted = true;
                    _context.Set<T>().Update(entity);
                }else
                {
                    //Hard Delete => for entities that don't support soft delete
                    _context.Set<T>().Remove(entity);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
