using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public class Module
    {
        public Module()
        {
            PlannedActivities = new HashSet<PlannedActivity>();
        }

        public int ModuleId { get; set; }

        public int StartDateId { get; set; }

        public int EndDateId { get; set; }

        public int Year { get; set; }

        public int Semester { get; set; }

        [StringLength(2)]
        [Column("Module")]
        public string ModuleText { get; set; }

        [StringLength(7)]
        public string Text { get; set; }

        public virtual Date StartDate { get; set; }

        public virtual Date EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlannedActivity> PlannedActivities { get; set; }
    }
}
