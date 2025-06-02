using PlannerServer.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class SummativeEvaluation
    {
        public SummativeEvaluation()
        {
            Evaluations = new HashSet<Evaluation>();
        }

        [Key]
        public int SumId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfSum { get; set; }

        [Required]
        [StringLength(20)]
        public string Module { get; set; }

        public int UserId { get; set; }

        public int StudentId { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }

        public virtual Student Student { get; set; }

        public virtual User User { get; set; }
    }
}
