using HRSystem.BLL.ModelVM.Department;

namespace HRSystem.BLL.Services.Abstraction
{
    public interface IDepartmentService
    {
        // Get all departments
        Task<Response<IEnumerable<DepartmentVM>>> GetAllDepartmentsAsync();

        // Get a department by ID
        Task <Response<DepartmentVM>> GetDepartmentByIdAsync(Guid id);
        Task<Response<DepartmentFormVM>> GetDepartmentForEditAsync(Guid id);

        // Add a new department
        Task<Response<bool>> AddDepartmentAsync(DepartmentFormVM departmentVM);

        // Update an existing department
        Task<Response<bool>> UpdateDepartmentAsync(DepartmentFormVM departmentVM);

        // Delete an existing department
        Task<Response<bool>> DeleteDepartmentAsync(Guid id);

    }
}
