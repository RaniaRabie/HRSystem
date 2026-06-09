using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Implementation
{
    public class LeaveRequestRepository: GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly AppDbContext _context;
        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.LeaveRequests
                .Include(l => l.LeaveType)
                .Include(l => l.Approver)
                    .ThenInclude(a => a!.User)
                .Where(l => l.EmployeeId == employeeId)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetPendingForApproverAsync(Guid approverId)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                    .ThenInclude(e => e.User)
                .Include(l => l.LeaveType)
                .Where(l => l.ApproverId == approverId
                         && l.Status == LeaveStatus.Pending)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<LeaveRequest?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                    .ThenInclude(e => e.User)
                .Include(l => l.LeaveType)
                .Include(l => l.Approver)
                    .ThenInclude(a => a!.User)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
