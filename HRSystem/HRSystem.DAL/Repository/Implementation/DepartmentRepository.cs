
namespace HRSystem.DAL.Repository.Implementation
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithPositons()
        {
            return await _context.Departments
                .Include(p => p.Positions)
                    .ThenInclude(e => e.Employees)
                        .ThenInclude(e => e.User)
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdWithItsManager(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return null;

                var entity = await _context.Departments
                    .Include(m=> m.Manager)
                    .FirstOrDefaultAsync(d => d.Id == id);

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


    }
}
