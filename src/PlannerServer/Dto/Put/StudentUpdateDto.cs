namespace PlannerServer.Dto.Put
{
    public class StudentUpdateDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unilogin { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int MunicipalityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime GraduationDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public int IdFromUms { get; set; }
        public string UmsActivity { get; set; } = string.Empty;
    }
}
