using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class Department : SoftDeletableEntity
    {
        public string Name { get; set; } = String.Empty;


        /* Relationship With Manager */
        public Guid? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        /* ------------------------------------------------ */
        // List Of Positions in this Department
        /* Relationship With Position */
        public ICollection<Position> Positions { get; set; } = new HashSet<Position>();

    }
}
