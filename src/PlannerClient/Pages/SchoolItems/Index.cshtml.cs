using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlannerClient.Pages.SchoolItems
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClient = httpClientFactory.CreateClient("PlannerApi");
            _logger = logger;
        }

        public List<SchoolItemDto> Items { get; set; } = new();
        public List<StudentDto> Students { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                Items = await _httpClient.GetFromJsonAsync<List<SchoolItemDto>>("api/schoolitems") ?? new();
                Students = await _httpClient.GetFromJsonAsync<List<StudentDto>>("api/students") ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch school items or students.");
                Items = new();
                Students = new();
            }
        }

        public async Task<IActionResult> OnPostAssignAsync(int ItemId, int StudentId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/schoolitems/{ItemId}/assign/{StudentId}", null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to assign item.");
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnassignAsync(int ItemId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/schoolitems/{ItemId}/unassign", null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to unassign item.");
            }

            return RedirectToPage();
        }

        public class SchoolItemDto
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

        public class StudentDto
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
}
