using PlannerServer.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class TeamStudent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }

        public string ExternalCensor { get; set; }

        public string SubjectDescription { get; set; }

        public int? GradeId { get; set; }

        public int? InternalCensorId { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual Student Student { get; set; }

        public virtual Team Team { get; set; }

        public virtual User InternalCensor { get; set; }
    }
}
