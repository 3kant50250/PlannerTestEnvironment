namespace PlannerServer.Models
{
    public partial class GrantInformation
    {
        public int GrantInformationId { get; set; }
        public int StudentId { get; set; }
        public int GrantKindId { get; set; }
        public int MunicipalityId { get; set; }
        public string CounselorName { get; set; } = string.Empty;
        public string CounselorPhone { get; set; } = string.Empty;
        public string CounselorEmail { get; set; } = string.Empty;

        public virtual Student? Student { get; set; }
        public virtual GrantKind? GrantKind { get; set; }
        public virtual Municipality? Municipality { get; set; }
    }
}
