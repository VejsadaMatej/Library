using LibraryGUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryGUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LibraryAPI");
        }

        public async Task<IActionResult> Index()
        {
            var statistics = await _httpClient.GetFromJsonAsync<StatisticsViewModel>("/api/statistics");
            return View(statistics);
        }
    }
}
