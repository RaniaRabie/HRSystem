using HRSystem.BLL.ModelVM.LeaveType;
using HRSystem.BLL.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.MVC.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        /* -------------------------------------------------------------- */
        // Index
        public async Task<IActionResult> Index()
        {
            var response = await _leaveTypeService.GetAllAsync();
            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<LeaveTypeVM>());
            }

            return View(response.Value);
        }
        /* -------------------------------------------------------------- */
        // Create

        [HttpGet]
        public IActionResult Create()
        {
            return View("Form", new LeaveTypeFormVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveTypeFormVM model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var response = await _leaveTypeService.CreateAsync(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage!);
                return View("Form", model);
            }

            TempData["Success"] = "Leave type created successfully";
            return RedirectToAction(nameof(Index));
        }

        /* -------------------------------------------------------------- */
        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _leaveTypeService.GetForEditAsync(id);
            if (!response.IsSuccess)
                return NotFound();

            return View("Form", response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LeaveTypeFormVM model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var response = await _leaveTypeService.UpdateAsync(model);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage!);
                return View("Form", model);
            }

            TempData["Success"] = "Leave type updated successfully";
            return RedirectToAction(nameof(Index));
        }

        /* -------------------------------------------------------------- */
        // Delete

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _leaveTypeService.DeleteAsync(id);

            TempData[response.IsSuccess ? "Success" : "Error"] = response.IsSuccess
                ? "Leave type deleted successfully"
                : response.ErrorMessage;

            return RedirectToAction(nameof(Index));
        }


    }
}
