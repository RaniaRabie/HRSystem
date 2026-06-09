
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BLL.ModelVM.Department
{
    public class DepartmentFormVM
    {
        /* --------------------------------------------------------------------- */
        public Guid? Id { get; set; } // null = create, has value = edit

        /* --------------------------------------------------------------------- */

        [Required(ErrorMessage = "Department name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; } = string.Empty;

        /* --------------------------------------------------------------------- */
        public Guid? ManagerId { get; set; }

        /* --------------------------------------------------------------------- */
    }
}
