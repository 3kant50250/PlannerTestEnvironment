using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Goal
    {
        public int GoalId { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string GoalDefinition { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string LearningObjective { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string GoalContent { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Criteria { get; set; }

        public int TypeOfGoals { get; set; }

        public int FormId { get; set; }

        public virtual FormativeEvaluation FormativeEvaluation { get; set; }
    }
}
