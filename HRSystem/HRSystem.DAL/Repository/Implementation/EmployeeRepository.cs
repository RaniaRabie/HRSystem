using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Implementation
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee?> GetEmployeeByIdWithDetailsAsync(Guid id)
        {
            return await _context.Employees
               .Include(e => e.User)
               .Include(e => e.Position)
                   .ThenInclude(p => p.Department)
               .Include(e => e.Supervisor)
                   .ThenInclude(s => s.User)
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetEmployeeByUserIdAsync(string userId)
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                .Include(e => e.Supervisor)
                    .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithDetailsAsync()
        {
            return await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                .Include(e => e.Supervisor)
                    .ThenInclude(s => s.User)
                .ToListAsync();

        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees
            .IgnoreQueryFilters() 
            .CountAsync();
        }
    }
}
