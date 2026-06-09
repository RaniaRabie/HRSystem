using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Implementation
{
    internal class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {

        private readonly AppDbContext _context;
        public LeaveTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveType>> GetAllWithRequestsCountAsync()
        {
            return await _context.LeaveTypes
                .Include(lt => lt.LeaveRequests)
                .ToListAsync();
        }
    }
}
