using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Dto.Post
{
    public class StudentCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Unilogin { get; set; } = string.Empty;
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int MunicipalityId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime GraduationDate { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string SerialNumber { get; set; } = string.Empty;
        [Required]
        public int IdFromUms { get; set; }
        [Required]
        public string UmsActivity { get; set; } = string.Empty;
    }
}
