namespace HRSystem.MVC.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class DepartmentController : Controller
    {
        /* ---------------------------------------------------------------------------------- */

        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        public DepartmentController(IDepartmentService departmentService,
            IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        /* ---------------------------------------------------------------------------------- */
        // Get All

        public async Task<IActionResult> Index()
        {
            var response = await _departmentService.GetAllDepartmentsAsync();
            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<DepartmentVM>());

            }

            return View(response.Value);
        }

        /* ---------------------------------------------------------------------------------- */
        // Create 

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await ManagersDropdown();
            return View("Form", new DepartmentFormVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await ManagersDropdown();
                return View("Form", model);
            }

            var response = await _departmentService.AddDepartmentAsync(model);
            if (!response.IsSuccess)
            {

                ModelState.AddModelError(string.Empty, response.ErrorMessage);
                await ManagersDropdown();
                return View("Form", model);
            }

            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Update

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _departmentService.GetDepartmentForEditAsync(id);
            if (!response.IsSuccess)
                return NotFound();

            await ManagersDropdown();

            var model = new DepartmentFormVM
            {
                Id = response.Value.Id,
                Name = response.Value.Name,
                ManagerId = response.Value.ManagerId
            };

            return View("Form", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await ManagersDropdown();
                return View("Form", model);
            }
            var response = await _departmentService.UpdateDepartmentAsync(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                await ManagersDropdown();
                return View("Form", model);
            }
            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Delete 

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _departmentService.DeleteDepartmentAsync(id);
            if (!response.IsSuccess)
                TempData["Error"] = response.ErrorMessage;
            else
                TempData["Success"] = "Department deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // ManagersDropdown

        private async Task ManagersDropdown()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.Managers = employees.Value.Select(e => new
            {
                e.Id,
                e.FullName
            });
        }
    }
}
