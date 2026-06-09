using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            // configure the Primart Kay
            builder.HasKey(p => p.Id);

            // configure the properties
            builder.Property(p => p.BaseSalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Bonus)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Deductions)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.NetSalary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // configure 
            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }


    }
}
