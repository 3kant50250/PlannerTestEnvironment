using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Models
{
    public partial class Team 
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }

       public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
