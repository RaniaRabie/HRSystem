namespace HRSystem.MVC.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class PositionController : Controller
    {
        /* ---------------------------------------------------------------------------------- */

        private readonly IPositionService _positionService;
        private readonly IDepartmentService _departmentService;
        public PositionController(IPositionService positionService,
            IDepartmentService departmentService
            )
        {
            _positionService = positionService;
            _departmentService = departmentService;
        }


        /* ---------------------------------------------------------------------------------- */
        // Get All

        public async Task<IActionResult> Index()
        {
            var response = await _positionService.GetAllPositionsAsync();
            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<PositionVM>());

            }

            return View(response.Value);
        }

        /* ---------------------------------------------------------------------------------- */
        // Create 

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await DepartmentsDropdown();
            return View("Form", new PositionFormVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await DepartmentsDropdown();
                return View("Form", model);
            }

            var response = await _positionService.AddPositionAsync(model);
            if (!response.IsSuccess)
            {

                ModelState.AddModelError(string.Empty, response.ErrorMessage);
                await DepartmentsDropdown();
                return View("Form", model);
            }

            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Update

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _positionService.GetPositionByIdAsync(id);
            if (!response.IsSuccess)
                return NotFound();

            await DepartmentsDropdown();

            var model = new PositionFormVM
            {
                Id = response.Value.Id,
                Name = response.Value.Name,
                DepartmentId = response.Value.DepartmentId
            };

            return View("Form", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PositionFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await DepartmentsDropdown();
                return View("Form", model);
            }
            var response = await _positionService.UpdatePositionAsync(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                await DepartmentsDropdown();
                return View("Form", model);
            }
            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // Delete 

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _positionService.DeletePositionAsync(id);
            if (!response.IsSuccess)
                TempData["Error"] = response.ErrorMessage;
            else
                TempData["Success"] = "Position deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        /* ---------------------------------------------------------------------------------- */
        // DepartmentsDropdown

        private async Task DepartmentsDropdown()
        {
            BLL.ResponseResult.Response<IEnumerable<DepartmentVM>>? departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = departments.Value.Select(d => new
            {
                d.Id,
                d.Name,
            });
        }
    }
}
