
using HRSystem.DAL.Entities;

namespace HRSystem.DAL.Context
{
    internal class HRSystemDbContext: DbContext
    {
        // define DbSet properties for your entities here
        // 1. use sql server
        // 2. connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=HRSystem;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations can be added here if needed

            // Configuration Files

            // this way to apply all configuration files in the assembly without needing to specify each one individually, it will automatically find and apply all classes that implement IEntityTypeConfiguration<T> in the assembly of AppContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRSystemDbContext).Assembly); // way 2



            // global query filter
            modelBuilder.Entity<Employee>().HasQueryFilter(s => !s.IsDeleted); // this will filter out any employees that are marked as deleted (IsDeleted = true) from all queries that retrieve employees from the database, ensuring that only active employees are returned in query results.


            
        }


        /* -----------------------------------------------------*/


        // tables اللي في وقت ما هتبقي classes محتاجين هنا نححدله مين
        // Nameing convention: class => singular, table => plural of class name
        // local container for the table of employees

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        

    }
}
