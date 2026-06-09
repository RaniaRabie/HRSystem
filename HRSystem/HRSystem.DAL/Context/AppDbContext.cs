using HRSystem.DAL.Models.Base;
using HRSystem.DAL.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRSystem.DAL.Context
{
    public class AppDbContext: IdentityDbContext<HRSystemUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations can be added here if needed

            // Configuration Files
            // this way to apply all configuration files in the assembly without needing to specify each one individually, it will automatically find and apply all classes that implement IEntityTypeConfiguration<T> in the assembly of AppContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); // way 2
        }

        public override int SaveChanges()
        {
            FillAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FillAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void FillAuditFields()
        {
            // get current user
            var currentUser = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";

            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in entries) { 
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = currentUser;
                }
                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = currentUser;
                }
                if (entry.Entity is SoftDeletableEntity softDelete &&  softDelete.IsDeleted
                    && softDelete.DeletedAt == null)
                {
                    softDelete.DeletedAt = DateTime.UtcNow;
                    softDelete.DeletedBy = currentUser;
                }

            }

        }


        /* -----------------------------------------------------*/


        // tables اللي في وقت ما هتبقي classes محتاجين هنا نححدله مين
        // Nameing convention: class => singular, table => plural of class name
        // local container for the table of employees

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

    }
}
