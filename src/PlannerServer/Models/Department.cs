using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Users = new HashSet<User>();
            SchoolItems = new HashSet<SchoolItem>();
        }

        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<SchoolItem> SchoolItems { get; set; }
    }
}
