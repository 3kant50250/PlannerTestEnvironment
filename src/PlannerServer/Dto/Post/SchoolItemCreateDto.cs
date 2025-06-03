using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Dto.Post
{
    public class SchoolItemCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

}
