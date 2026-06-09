using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            // Configure the primary key
            builder.HasKey(p => p.Id);

            // Configure the Name property
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            // configure the relationship with Department
            builder.HasOne(p => p.Department)
                .WithMany(d => d.Positions)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Global query filter for soft deletion
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
