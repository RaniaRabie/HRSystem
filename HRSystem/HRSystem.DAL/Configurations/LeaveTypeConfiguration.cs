using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Configurations
{
    internal class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasQueryFilter(l => !l.IsDeleted);

            builder.ToTable("LeaveTypes");
        }
    }
}
