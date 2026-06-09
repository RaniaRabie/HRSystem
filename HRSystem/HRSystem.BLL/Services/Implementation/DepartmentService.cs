using HRSystem.BLL.ModelVM.Department;

namespace HRSystem.BLL.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        /* ------------------------------------------------------------------------------------ */

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /* ------------------------------------------------------------------------------------ */

        public DepartmentService(IDepartmentRepository departmentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<IEnumerable<DepartmentVM>>> GetAllDepartmentsAsync()
        {
            try
            {
                var result = await _departmentRepository.GetDepartmentsWithPositons();

                var mapp = result.Select(d => new DepartmentVM
                {
                    Id = d.Id,
                    Name = d.Name,
                    MgrName = d.Manager != null
                    ? $"{d.Manager.User.FullName}"
                    : "No Manager",
                    PositionsCount = d.Positions.Count,
                    EmployeeCount = d.Positions.Sum(p => p.Employees.Count)
                });



                return new Response<IEnumerable<DepartmentVM>>(mapp, null, true);

            }
            catch (Exception ex) {
            
                return new Response<IEnumerable<DepartmentVM>>(null, ex.Message, false);
            }
        }


        /* ------------------------------------------------------------------------------------ */

        // For UI Disply
        public async Task<Response<DepartmentVM>> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var result = await _departmentRepository.GetDepartmentByIdWithItsManager(id);

                if (result == null)
                    return new Response<DepartmentVM>(null, "Department not found", false);
                else
                {

                    var mapp = new DepartmentVM
                    {
                        Id = result.Id,
                        Name = result.Name,
                        MgrName = result.Manager != null
                            ? $"{result.Manager.User.FullName}"
                            : "No Manager"
                    };

                    return new Response<DepartmentVM>(mapp, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<DepartmentVM>(null, ex.Message, false);
            }
        }


        // For Edit
        public async Task<Response<DepartmentFormVM>> GetDepartmentForEditAsync(Guid id)
        {
            try
            {
                var result = await _departmentRepository.GetDepartmentByIdWithItsManager(id);

                if (result == null)
                    return new Response<DepartmentFormVM>(null, "Department not found", false);
                else
                {

                    DepartmentFormVM? mapp = new DepartmentFormVM
                    {
                        Id = result.Id,
                        Name = result.Name,
                        ManagerId = result.ManagerId
                    };

                    return new Response<DepartmentFormVM>(mapp, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<DepartmentFormVM>(null, ex.Message, false);
            }
        }
        /* ------------------------------------------------------------------------------------ */

        public async Task<Response<bool>> AddDepartmentAsync(DepartmentFormVM departmentVM)
        {
            
            try
            {
                var department = new Department
                {
                    Name = departmentVM.Name,
                    ManagerId = departmentVM.ManagerId,
                };


                await _departmentRepository.AddAsync(department);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }


        /* ------------------------------------------------------------------------------------ */


        public async Task<Response<bool>> UpdateDepartmentAsync(DepartmentFormVM departmentVM)
        {
            try
            {
                var department = await _departmentRepository.GetByIdAsync(departmentVM.Id.Value);
                if (department == null)
                {
                    return new Response<bool>(false, "Department not found", false);
                }
                

                department.Name = departmentVM.Name;
                department.ManagerId = departmentVM.ManagerId;

                _departmentRepository.Update(department);
                await _unitOfWork.SaveChangesAsync();

                return new Response<bool>(true, null, true);

            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);

            }
        }

        /* ------------------------------------------------------------------------------------ */


        public async Task<Response<bool>> DeleteDepartmentAsync(Guid id)
        {
            try
            {
                var dept = await _departmentRepository.GetByIdAsync(id);
                if (dept == null)
                {
                    return new Response<bool>(false, "Department not found", false);
                }
                else
                {

                    _departmentRepository.Delete(dept);
                    await _unitOfWork.SaveChangesAsync();

                    return new Response<bool>(true, null, true);
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }
    }
}
