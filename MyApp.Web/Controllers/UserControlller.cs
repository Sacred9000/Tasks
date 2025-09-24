using Microsoft.AspNetCore.Mvc;
using MyApp.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
namespace MyApp.Web.Controllers

{
    public class UsersController : Controller
    {
        private readonly HttpClient _http;

        public UsersController(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("MyAppApi");
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            var users = await _http.GetFromJsonAsync<List<User>>("api/users");
            return View(users);
        }

        // GET: /Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _http.GetFromJsonAsync<User>($"api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var response = await _http.PostAsJsonAsync("api/users", user);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: /Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _http.GetFromJsonAsync<User>($"api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _http.PutAsJsonAsync($"api/users/{id}", user);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: /Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _http.GetFromJsonAsync<User>($"api/users/{id}");
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _http.DeleteAsync($"api/users/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}