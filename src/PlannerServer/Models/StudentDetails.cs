namespace PlannerServer.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public string Firstnames { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string CPR { get; set; }
        public string UniLogin { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string MobilePhone { get; set; }
        public string PrivateMail { get; set; }
        public string HasSecretAddress { get; set; }
    }
}
