using HRSystem.BLL.ModelVM.Dashboard;

namespace HRSystem.BLL.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DashboardService(
            IDepartmentRepository departmentRepository,
            IPositionRepository positionRepository,
            IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _positionRepository = positionRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Response<DashboardVM>> GetAdminDashboardAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesWithDetailsAsync();
                var totalEmployeesCount = await _employeeRepository.GetTotalCountAsync();
                var departments = await _departmentRepository.GetDepartmentsWithPositons();
                var positions = await _positionRepository.GetAllAsync();

                var vm = new DashboardVM
                {
                    TotalEmployees = totalEmployeesCount,
                    TotalDepartments = departments.Count(),
                    TotalPositions = positions.Count(),
                    ActiveUsers = employees.Count(e => !e.IsDeleted),
                    RecentEmployees = employees
                        .OrderByDescending(e => e.CreatedAt)
                        .Take(5)
                        .Select(e => new RecentEmployeeVM
                        {
                            FullName = e.User.FullName,
                            Email = e.User.Email!,
                            PositionName = e.Position.Name,
                            DepartmentName = e.Position.Department.Name,
                            IsActive = !e.IsDeleted
                        })
                };

                return new Response<DashboardVM>(vm, null, true);
            }
            catch (Exception ex)
            {
                return new Response<DashboardVM>(null, ex.Message, false);
            }
        }

        public async Task<Response<DashboardVM>> GetEmployeeDashboardAsync(string userId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByUserIdAsync(userId);

                if (employee == null)
                    return new Response<DashboardVM>(null, "Employee not found", false);

                var vm = new DashboardVM
                {
                    MyProfile = new EmployeeProfileVM
                    {
                        FullName = employee.User.FullName,
                        Email = employee.User.Email!,
                        PositionName = employee.Position.Name,
                        DepartmentName = employee.Position.Department.Name,
                        SupervisorName = employee.Supervisor?.User.FullName,
                        Salary = employee.Salary,
                        HireDate = employee.HireDate
                    }
                };

                return new Response<DashboardVM>(vm, null, true);
            }
            catch (Exception ex)
            {
                return new Response<DashboardVM>(null, ex.Message, false);
            }
        }
    }
}
