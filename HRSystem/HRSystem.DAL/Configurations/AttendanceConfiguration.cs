using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            // Configure the primary key
            builder.HasKey(a => a.Id);

            // Configure the relationship between Attendance and Employee
            builder.HasOne(a => a.Employee)
           .WithMany(e => e.Attendances)
           .HasForeignKey(a => a.EmployeeId)
           .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
