using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public class SchoolItem
    {
        public int SchoolItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public int DepartmentId { get; set; }
        public int? StudentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Student Student { get; set; }
    }
}
