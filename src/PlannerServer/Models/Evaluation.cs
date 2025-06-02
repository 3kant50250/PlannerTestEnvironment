using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Evaluation
    {
        [Key]
        public int EvalId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string SubjectEval { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PersonalEval { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string WorkEval { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string OtherEval { get; set; }

        public int SummativePerson { get; set; }

        public int SumId { get; set; }

        public virtual SummativeEvaluation SummativeEvaluation { get; set; }
    }
}
