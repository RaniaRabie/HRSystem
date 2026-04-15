using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class Department : SoftDeletableEntity
    {
        public string Name { get; set; } = null!;


        /* Relationship With Manager */
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        /* ------------------------------------------------ */
        // List Of Positions in this Department
        /* Relationship With Position */
        public ICollection<Position> Positions { get; set; } = new HashSet<Position>();

    }
}
