using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Abstraction
{
    public interface ILeaveTypeRepository: IGenericRepository<LeaveType>
    {
        Task<IEnumerable<LeaveType>> GetAllWithRequestsCountAsync();

    }
}
