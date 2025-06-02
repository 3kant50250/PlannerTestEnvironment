namespace PlannerServer.Models
{
    public partial class GrantKind
    {
        public int GrantKindId { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<GrantInformation>? GrantInformations { get; set; }
    }
}
