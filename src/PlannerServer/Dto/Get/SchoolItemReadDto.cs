namespace PlannerServer.Dto.Get
{
    public class SchoolItemReadDto
    {
        public int SchoolItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public int DepartmentId { get; set; }
        public int? StudentId { get; set; }

        public string? DepartmentName { get; set; }
        public string? StudentName { get; set; }
    }

}
