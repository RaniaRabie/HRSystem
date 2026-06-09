using HRSystem.BLL.ModelVM.Employee;

namespace HRSystem.MVC.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;

        public EmployeeController(IEmployeeService employeeService, IPositionService positionService)
        {
            _employeeService = employeeService;
            _positionService = positionService;
        }


        /* ---------------------------------------------------------------------------------- */
        // Get All

        public async Task<IActionResult> Index(Guid? deptId)
        {
            var response = deptId.HasValue
                ? await _employeeService.GetEmployeeByDepartmentAsync(deptId.Value)
                : await _employeeService.GetAllEmployeesAsync();

            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<EmployeeVM>());

            }

            return View(response.Value);
        }

        /* ---------------------------------------------------------------------------------- */
        // Create 

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PositionsAndSupervisorsDropdown();
            return View("Form", new EmployeeFormVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await PositionsAndSupervisorsDropdown();
                return View("Form", model);
            }

            var response = await _employeeService.CreateAsync(model);
            if (!response.IsSuccess)
            {

                ModelState.AddModelError(string.Empty, response.ErrorMessage);
                await PositionsAndSupervisorsDropdown ();
                return View("Form", model);
            }

            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Update

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _employeeService.GetEmployeeForEditAsync(id);
            if (!response.IsSuccess)
                return NotFound();

            await PositionsAndSupervisorsDropdown();

             var model = new EmployeeFormVM
            {
                 Id = response.Value.Id,
                 FirstName = response.Value.FirstName,
                 LastName = response.Value.LastName,
                 Email = response.Value.Email,
                 Salary = response.Value.Salary,
                 HireDate = response.Value.HireDate,
                 PositionId = response.Value.PositionId,
                 SupervisorId = response.Value.SupervisorId
             };

            return View("Form", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await PositionsAndSupervisorsDropdown();
                return View("Form", model);
            }
            var response = await _employeeService.UpdateAsync(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                await PositionsAndSupervisorsDropdown();

                return View("Form", model);
            }
            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Deactivate
        [HttpPost]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var response = await _employeeService.DeactivateEmployeeAsync(id);
            if (!response.IsSuccess)
                TempData["Error"] = response.ErrorMessage;
            else
                TempData["Success"] = "Employee deleted successfully";

            return RedirectToAction(nameof(Index));
        }


        // Activate 

        [HttpPost]
        public async Task<IActionResult> Activate(Guid id)
        {
            var response = await _employeeService.ActivateEmployeeAsync(id);
            if (!response.IsSuccess)
                TempData["Error"] = response.ErrorMessage;
            else
                TempData["Success"] = "Employee Added successfully";

            return RedirectToAction(nameof(Index));
        }

        

        /* ---------------------------------------------------------------------------------- */
        // ManagersDropdown

        private async Task PositionsAndSupervisorsDropdown()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var positions = await _positionService.GetAllPositionsAsync();

            ViewBag.Positions = positions.Value.Select(p => new
            {
                p.Id,
                p.Name,
                p.DepartmentName
            });

            ViewBag.Supervisors = employees.Value.Select(e => new
            {
                e.Id,
                e.FullName
            });
        }
    }
}
