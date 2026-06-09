namespace HRSystem.BLL.Services.Abstraction
{
    public interface IEmployeeService
    {
        Task<Response<IEnumerable<EmployeeVM>>> GetAllEmployeesAsync();

        Task<Response<IEnumerable<EmployeeVM>>> GetEmployeeByDepartmentAsync(Guid departmentId);
        Task<Response<EmployeeVM>> GetEmployeeByIdAsync(Guid id);
        Task<Response<EmployeeFormVM>> GetEmployeeForEditAsync(Guid id);

        Task<Response<bool>> CreateAsync(EmployeeFormVM employee);
        Task<Response<bool>> UpdateAsync(EmployeeFormVM employee);
        Task<Response<bool>> DeactivateEmployeeAsync(Guid id);
        Task<Response<bool>> ActivateEmployeeAsync(Guid id);
    }
}
