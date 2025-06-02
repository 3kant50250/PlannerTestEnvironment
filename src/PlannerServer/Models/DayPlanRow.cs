namespace PlannerServer.Models
{
    public partial class DayPlanRow
    {
        public int DayPlanRowId { get; set; }

        public string Theme { get; set; }

        public string LearningGoal { get; set; }

        public string Content { get; set; }

        public string Materials { get; set; }

        public int DayPlanId { get; set; }

        public virtual DayPlan DayPlan { get; set; }
    }
}
