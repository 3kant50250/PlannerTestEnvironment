using PlannerServer.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class FormativeEvaluation
    {
        public FormativeEvaluation()
        {
            Goals = new HashSet<Goal>();
        }

        [Key]
        public int FormId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfForm { get; set; }

        [Required]
        [StringLength(20)]
        public string Module { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Eval { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Wishes { get; set; }  // TODO: Rename to WishesNote

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public int? FirstPrioritySubjectId { get; set; }

        public int? SecondPrioritySubjectId { get; set; }

        public int? ThirdPrioritySubjectId { get; set; }

        public virtual Student Student { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }

        public virtual Subject FirstPrioritySubject { get; set; }

        public virtual Subject SecondPrioritySubject { get; set; }

        public virtual Subject ThirdPrioritySubject { get; set; }
    }
}
