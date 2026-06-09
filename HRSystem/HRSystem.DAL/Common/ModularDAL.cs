using HRSystem.DAL.Models.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace HRSystem.DAL.Common
{
    public static class ModularDAL
    {
        public static IServiceCollection AddServicesInDAL(this IServiceCollection services)
        {
            services.AddScoped< IDepartmentRepository, DepartmentRepository>();
            services.AddScoped< IPositionRepository, PositionRepository>();
            services.AddScoped< IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();    
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>(); 

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
