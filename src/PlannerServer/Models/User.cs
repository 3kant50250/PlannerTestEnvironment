using PlannerServer.Models;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Initials { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
