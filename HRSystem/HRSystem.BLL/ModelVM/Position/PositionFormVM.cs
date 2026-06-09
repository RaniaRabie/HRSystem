
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRSystem.BLL.ModelVM.Position
{
    public class PositionFormVM
    {
        /* ------------------------------------------------------------- */
        public Guid? Id { get; set; }

        /* ------------------------------------------------------------- */

        [Required(ErrorMessage = "Position name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; }

        /* ------------------------------------------------------------- */

        [Required(ErrorMessage = "Department is required")]
        public Guid DepartmentId { get; set; }

        /* ------------------------------------------------------------- */
    }
}
