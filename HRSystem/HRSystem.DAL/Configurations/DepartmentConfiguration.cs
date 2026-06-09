using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Configure the primary key
            builder.HasKey(d => d.Id);

            // Configure the Name property
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the relationship with Employee (Manager)
            builder.HasOne(d => d.Manager)
                .WithMany(e => e.ManagedDepartments)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Global query filter for soft deletion
            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}
