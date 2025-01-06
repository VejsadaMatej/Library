using LibraryGUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryGUI.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BorrowedBooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LibraryAPI");
        }

        // GET: /BorrowedBooks
        public async Task<IActionResult> Index()
        {
            var borrows = await _httpClient.GetFromJsonAsync<List<BorrowedBook>>("/api/borrow");
            return View(borrows);
        }

        // GET: /BorrowedBooks/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var borrow = await _httpClient.GetFromJsonAsync<BorrowedBook>($"/api/borrow/{id}");
            if (borrow == null) return NotFound();

            return View(borrow);
        }

        // GET: /BorrowedBooks/Create
        public async Task<IActionResult> Create()
        {
            // Načítání seznamu knih a uživatelů
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("/api/books");
            var users = await _httpClient.GetFromJsonAsync<List<User>>("/api/users");
            ViewBag.Books = books;
            ViewBag.Users = users;

            return View();
        }

        // POST: /BorrowedBooks/Create
        [HttpPost]
        public async Task<IActionResult> Create(BorrowedBook borrow)
        {
            if (!ModelState.IsValid) return View(borrow);

            var response = await _httpClient.PostAsJsonAsync("/api/borrow", borrow);
            if (!response.IsSuccessStatusCode) return View(borrow);

            return RedirectToAction(nameof(Index));
        }

        // GET: /BorrowedBooks/History
        public async Task<IActionResult> History()
        {
            var history = await _httpClient.GetFromJsonAsync<List<BorrowedBook>>("/api/borrow/history");
            return View(history);
        }

        // POST: /BorrowedBooks/Return/{id}
        [HttpPost]
        public async Task<IActionResult> Return(int id)
        {
            var borrow = await _httpClient.GetFromJsonAsync<BorrowedBook>($"/api/borrow/{id}");
            if (borrow == null) return NotFound();

            borrow.ReturnDate = DateTime.Now;

            var response = await _httpClient.PutAsJsonAsync($"/api/borrow/{id}", borrow);
            return RedirectToAction(nameof(Index));
        }
    }
}
