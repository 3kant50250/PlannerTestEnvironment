using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Student
    {
        public Student()
        {
            SchoolItems = new HashSet<SchoolItem>();
        }

        public int StudentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Unilogin { get; set; }

        public int DepartmentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime GraduationDate { get; set; }

        public int MunicipalityId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }

        [StringLength(4)]
        public string SerialNumber { get; set; }

        public int? IdFromUms { get; set; }

        [StringLength(256)]
        public string UmsActivity { get; set; }

        public virtual Department Department { get; set; }
        public virtual Municipality Municipality { get; set; }
        public virtual ContactInformation? ContactInformation { get; set; }
        public virtual ICollection<StudentEnrollment>? StudentEnrollments { get; set; }
        public virtual ICollection<StudentGraduation>? StudentGraduations { get; set; }
        public virtual ICollection<GrantInformation>? GrantInformations { get; set; }
        public virtual ICollection<SchoolItem> SchoolItems { get; set; }
    }
}
