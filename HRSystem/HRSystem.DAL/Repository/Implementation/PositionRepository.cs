namespace HRSystem.DAL.Repository.Implementation
{
    public class PositionRepository: GenericRepository<Position>, IPositionRepository
    {
        /* ----------------------------------------------------------------------------------------*/


        private readonly AppDbContext _context;

        public PositionRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

        /* ----------------------------------------------------------------------------------------*/

        public async Task<IEnumerable<Position>> GetPositionssWithDepartmentNamesAndNumOfEmployees()
        {
            return await _context.Positions
                .Include(p => p.Department)
                .Include(p => p.Employees)
                .ToListAsync();
        }

        public async Task<Position?> GetPositionByIdWithItsRelatedDepartment(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return null;

                var entity = await _context.Positions
                    .Include(p => p.Department)
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
