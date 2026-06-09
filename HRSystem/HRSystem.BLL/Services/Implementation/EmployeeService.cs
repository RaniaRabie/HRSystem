using Microsoft.AspNetCore.Identity;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HRSystem.BLL.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        // we deal with repositories in the service layer, so we need to inject them here

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<HRSystemUser> _userManager;

        /* ------------------------------------------------------------------------------------ */

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<HRSystemUser> userManager)
        {
            _employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<IEnumerable<EmployeeVM>>> GetAllEmployeesAsync()
        {
            try
            {
                var result = await _employeeRepository.GetEmployeesWithDetailsAsync();

                var mapp = result.Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    FullName = e.User.FullName,
                    Email = e.User.Email!,
                    PositionName = e.Position.Name,
                    DepartmentName = e.Position.Department.Name,
                    Salary = e.Salary,
                    HireDate = e.HireDate,
                    IsActive = !e.IsDeleted,
                    SupervisorName = e.Supervisor?.User?.FullName ?? "No Supervisor"
                });

                return new Response<IEnumerable<EmployeeVM>>(mapp, null, true);
            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<EmployeeVM>>(null, ex.Message, false);

            }
        }


        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<IEnumerable<EmployeeVM>>> GetEmployeeByDepartmentAsync(Guid departmentId)
        {
            try
            {
                var result = await _employeeRepository
                .GetAllAsync(e => e.Position.DepartmentId == departmentId);

                var mapp = result.Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    FullName = e.User.FullName,
                    Email = e.User.Email!,
                    PositionName = e.Position.Name,
                    DepartmentName = e.Position.Department.Name,
                    Salary = e.Salary,
                    HireDate = e.HireDate,
                    IsActive = !e.IsDeleted,
                });

                return new Response<IEnumerable<EmployeeVM>>(mapp, null, true);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<EmployeeVM>>(null, ex.Message, false);

            }
        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<EmployeeVM>> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var result = await _employeeRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return new Response<EmployeeVM>(null, "Employee Not Found", true);
                }
                else
                {
                    var mapp = new EmployeeVM
                    {
                        Id = result.Id,
                        FullName = result.User.FullName,
                        Email = result.User.Email!,
                        PositionName = result.Position.Name,
                        DepartmentName = result.Position.Department.Name,
                        Salary = result.Salary,
                        HireDate = result.HireDate,
                        IsActive = !result.IsDeleted,
                        SupervisorName = result.Supervisor?.User.FullName
                    };

                    return new Response<EmployeeVM>(mapp, null, true);

                }
            }
            catch(Exception ex)
            {
                return new Response<EmployeeVM>(null, ex.Message, false);

            }

        }

        public async Task<Response<EmployeeFormVM>> GetEmployeeForEditAsync(Guid id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeByIdWithDetailsAsync(id);
                if (result == null)
                {
                    return new Response<EmployeeFormVM>(null, "Employee Not Found", false);
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(result.User);
                    var mapp = new EmployeeFormVM
                    {
                        Id = result.Id,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Email = result.User.Email!,
                        PositionId = result.PositionId,
                        Salary = result.Salary,
                        HireDate = result.HireDate,
                        SupervisorId = result.SupervisorId,
                        Role = roles.FirstOrDefault() ?? "Employee" 

                    };

                    return new Response<EmployeeFormVM>(mapp, null, true);

                }
            }
            catch (Exception ex)
            {
                return new Response<EmployeeFormVM>(null, ex.Message, false);

            }

        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<bool>> CreateAsync(EmployeeFormVM employee)
        {
            try
            {
                // Create Identity User
                var user = new HRSystemUser
                {
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    Email = employee.Email,
                    UserName = employee.Email
                };

                await _userManager.CreateAsync(user, employee.Password);
                await _userManager.AddToRoleAsync(user, employee.Role);

                // Create Employee record
                var emp = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Salary = employee.Salary,
                    HireDate = employee.HireDate,
                    UserId = user.Id,
                    PositionId = employee.PositionId,
                    SupervisorId = employee.SupervisorId
                };

                await _employeeRepository.AddAsync(emp);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }


        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<bool>> UpdateAsync(EmployeeFormVM employeeVM)
        {
            try
            {
                var oldEmployee = await _employeeRepository.GetByIdAsync(employeeVM.Id!.Value);
                if (oldEmployee is null)
                {
                    return new Response<bool>(false, "Employee not found", false);
                }

                oldEmployee.FirstName = employeeVM.FirstName;
                oldEmployee.LastName = employeeVM.LastName;
                oldEmployee.Salary = employeeVM.Salary;
                oldEmployee.HireDate = employeeVM.HireDate;
                oldEmployee.PositionId = employeeVM.PositionId;
                oldEmployee.SupervisorId = employeeVM.SupervisorId;

                // Update FullName in Identity
                oldEmployee.User.FullName = $"{employeeVM.FirstName} {employeeVM.LastName}";
                await _userManager.UpdateAsync(oldEmployee.User);

                // Update Role
                var currentRoles = await _userManager.GetRolesAsync(oldEmployee.User);
                await _userManager.RemoveFromRolesAsync(oldEmployee.User, currentRoles);
                await _userManager.AddToRoleAsync(oldEmployee.User, employeeVM.Role);

                _employeeRepository.Update(oldEmployee);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }


            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<bool>> ActivateEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee is null)
                {
                    return new Response<bool>(false, "Employee not found", false);
                }
                employee.IsDeleted = false;
                employee.DeletedAt = null;
                employee.DeletedBy = null;

                _employeeRepository.Update(employee);

                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);

            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }

        }



        public async Task<Response<bool>> DeactivateEmployeeAsync(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee is null)
                {
                    return new Response<bool>(false, "Employee not found", false);
                }

                
                _employeeRepository.Delete(employee);

                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);

            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }

    }
}
