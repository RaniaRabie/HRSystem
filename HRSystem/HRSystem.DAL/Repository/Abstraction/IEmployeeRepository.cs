
namespace HRSystem.DAL.Repository.Abstraction
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetEmployeeByUserIdAsync(string userId);
        public Task<IEnumerable<Employee>> GetEmployeesWithDetailsAsync();
        Task<Employee?> GetEmployeeByIdWithDetailsAsync(Guid id);
        Task<int> GetTotalCountAsync(); // includes deleted




    }
}
