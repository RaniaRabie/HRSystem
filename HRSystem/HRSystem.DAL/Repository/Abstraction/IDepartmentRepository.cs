using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Abstraction
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        public Task<IEnumerable<Department>> GetDepartmentsWithPositons();
        public Task<Department?> GetDepartmentByIdWithItsManager(Guid id);
    }
}
