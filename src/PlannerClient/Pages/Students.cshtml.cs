using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using PlannerClient.Models;

namespace PlannerClient.Pages;

public class StudentsModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public List<StudentDto> Students { get; set; } = new();

    public StudentsModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _clientFactory.CreateClient("PlannerApi");
        var result = await client.GetFromJsonAsync<List<StudentDto>>("/students");

        if (result is not null)
            Students = result;
    }
}
