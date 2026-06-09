using HRSystem.BLL.Mapper;
using HRSystem.BLL.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace HRSystem.BLL.Common
{
    public static class ModularBLL
    {
        public static IServiceCollection AddServicesInBLL(this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IEmailService, EmailService>();


            return services;
        }
    }
}
