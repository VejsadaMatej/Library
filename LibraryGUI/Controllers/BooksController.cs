using LibraryGUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryGUI.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LibraryAPI");
        }

        // GET: /Books
        public async Task<IActionResult> Index(string search = null)
        {
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("/api/books");

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         b.Author.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(books);
        }

        // GET: /Books/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"/api/books/{id}");
            if (book == null) return NotFound();

            return View(book);
        }

        // GET: /Books/Create
        public IActionResult Create() => View();

        // POST: /Books/Create
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid) return View(book);

            var response = await _httpClient.PostAsJsonAsync("/api/books", book);
            if (!response.IsSuccessStatusCode) return View(book);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Books/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"/api/books/{id}");
            if (book == null) return NotFound();

            return View(book);
        }

        // POST: /Books/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (!ModelState.IsValid) return View(book);

            var response = await _httpClient.PutAsJsonAsync($"/api/books/{id}", book);
            if (!response.IsSuccessStatusCode) return View(book);

            return RedirectToAction(nameof(Index));
        }

        // POST: /Books/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/books/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
