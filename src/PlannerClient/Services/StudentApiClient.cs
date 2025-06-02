using PlannerClient.Models;

namespace PlannerClient.Services
{
    public class StudentApiClient
    {
        private readonly HttpClient _http;

        public StudentApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            return await _http.GetFromJsonAsync<List<StudentDto>>("students") ?? new List<StudentDto>();
        }
    }
}
