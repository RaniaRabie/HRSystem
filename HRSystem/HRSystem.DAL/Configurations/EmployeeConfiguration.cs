using HRSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HRSystem.DAL.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            /* Configure the Employee entity */

            // Set the primary key
            builder.HasKey(e => e.Id);

            // FirstName and LastName are required with a maximum length of 50 characters
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength (50);

            builder.Property(e => e.Email)
                .IsRequired();

            
            builder.Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            // Configure the relationship with Position
            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the self-referencing relationship for Supervisor and Subordinates
            builder.HasOne(e => e.Supervisor)
                .WithMany(s => s.Subordinates)
                .HasForeignKey(e => e.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
