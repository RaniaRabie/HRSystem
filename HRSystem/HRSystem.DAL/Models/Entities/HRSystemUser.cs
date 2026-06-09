using Microsoft.AspNetCore.Identity;

namespace HRSystem.DAL.Models.Entities
{
    public class HRSystemUser: IdentityUser
    {
        // Navigation property to Employee, assuming one-to-one relationship
        public Employee Employee { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
