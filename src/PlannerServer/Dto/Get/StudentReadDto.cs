namespace PlannerServer.Dto.Get
{
    public class StudentReadDto
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }
        public string? Unilogin { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? MunicipalityId { get; set; }
        public string? SerialNumber { get; set; }
        public string? UmsActivity { get; set; }
    }
}
