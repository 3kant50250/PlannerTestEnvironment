using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Municipality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Municipality()
        {
            Students = new HashSet<Student>();
        }

        public int MunicipalityId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<GrantInformation> GrantInformations { get; set; }
    }
}
