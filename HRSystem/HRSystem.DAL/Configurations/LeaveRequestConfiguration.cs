using HRSystem.DAL.Models.Entities;

namespace HRSystem.DAL.Configurations
{
    internal class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Reason)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(l => l.ApproverNote)
                   .HasMaxLength(500);

            builder.Property(l => l.Status)
                   .HasConversion<string>(); // store as string not int


            // LeaveType → LeaveRequests
            builder.HasOne(l => l.LeaveType)
                   .WithMany(lt => lt.LeaveRequests)
                   .HasForeignKey(l => l.LeaveTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Employee → LeaveRequests (submitted)
            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.LeaveRequests)
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Approver → LeaveRequestsToApprove
            builder.HasOne(l => l.Approver)
                   .WithMany(e => e.LeaveRequestsToApprove)
                   .HasForeignKey(l => l.ApproverId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(l => !l.IsDeleted);

            builder.ToTable("LeaveRequests");

        }
    }
}
