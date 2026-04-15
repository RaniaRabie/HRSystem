using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class Position : SoftDeletableEntity
    {
        public string Name { get; set; } = null!;

        // Relationship With Department
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        /* ------------------------------------------------------- */

        // List Of Employees in this Position
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
