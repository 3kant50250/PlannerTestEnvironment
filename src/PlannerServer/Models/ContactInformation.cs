namespace PlannerServer.Models
{
    public partial class ContactInformation
    {
        public int ContactInformationId { get; set; }
        public int StudentId { get; set; }
        public string PrivatePhone { get; set; } = string.Empty;
        public string PrivateEmail { get; set; } = string.Empty;
        public string ParentNames { get; set; } = string.Empty;
        public string ParentPhones { get; set; } = string.Empty;
        public string ParentEmails { get; set; } = string.Empty;
        public string CurrentAddress { get; set; } = string.Empty;

        public virtual required Student Student { get; set; }
    }
}
