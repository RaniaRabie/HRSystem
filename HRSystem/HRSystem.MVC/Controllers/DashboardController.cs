using HRSystem.BLL.ModelVM.Dashboard;
using System.Security.Claims;

namespace HRSystem.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            // Employee role → get their own profile only
            if (User.IsInRole("Employee"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                var response = await _dashboardService.GetEmployeeDashboardAsync(userId);

                if (!response.IsSuccess)
                {
                    TempData["Error"] = response.ErrorMessage;
                    return View(new DashboardVM());
                }

                return View(response.Value);
            }

            // Admin or HR → get full stats
            else
            {
                var response = await _dashboardService.GetAdminDashboardAsync();

                if (!response.IsSuccess)
                {
                    TempData["Error"] = response.ErrorMessage;
                    return View(new DashboardVM());
                }

                return View(response.Value);
            }
        }
    }

}
