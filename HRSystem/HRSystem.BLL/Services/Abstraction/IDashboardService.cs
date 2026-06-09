using HRSystem.BLL.ModelVM.Dashboard;

namespace HRSystem.BLL.Services.Abstraction
{
    public interface IDashboardService
    {
        Task<Response<DashboardVM>> GetAdminDashboardAsync();
        Task<Response<DashboardVM>> GetEmployeeDashboardAsync(string userId);
    }
}
