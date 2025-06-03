using System.ComponentModel.DataAnnotations;

namespace PlannerServer.Dto.Put
{
    public class SchoolItemUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public int? StudentId { get; set; }
    }

}
