using Microsoft.AspNetCore.Mvc;
using MyApp.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Web.Controllers
{
    public class GroupsController : Controller
    {
        private readonly HttpClient _http;

        public GroupsController(IHttpClientFactory clientFactory)
        {
            _http = clientFactory.CreateClient("MyAppApi");
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var groups = await _http.GetFromJsonAsync<List<Group>>("api/group");
            var groupCount = await _http.GetFromJsonAsync<int>("api/group/count");
            var usersCountPerGroup = await _http.GetFromJsonAsync<List<GroupUserCount>>("api/group/userscount");

            var viewModel = new GroupIndexViewModel
            {
                Groups = groups ?? new List<Group>(),
                TotalGroupCount = groupCount,
                UsersCountPerGroup = usersCountPerGroup ?? new List<GroupUserCount>()
            };

            return View(viewModel);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var group = await _http.GetFromJsonAsync<Group>($"api/group/{id}");
            if (group == null)
                return NotFound();

            var users = await _http.GetFromJsonAsync<List<User>>($"api/usergroups/group/{id}");
            var viewModel = new GroupDetailsViewModel
            {
                Group = group,
                Members = users ?? new List<User>()
            };

            return View(viewModel);
        }

        // GET: Groups/AddMember/5
        public async Task<IActionResult> AddMember(int groupId)
        {
            var allUsers = await _http.GetFromJsonAsync<List<User>>("http://localhost:5009/api/users") ?? new List<User>();

            ViewBag.AllUsers = allUsers.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Username
            }).ToList();
            ViewBag.GroupId = groupId;

            return View(new AddMemberViewModel { GroupId = groupId });
        }

        // POST: Groups/AddMember
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(AddMemberViewModel model){
            if (!ModelState.IsValid)
            {   

                // repopulate users for the dropdown
                var allUsers = await _http.GetFromJsonAsync<List<User>>("http://localhost:5009/api/users") ?? new List<User>();
                ViewBag.AllUsers = allUsers.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Username
                }).ToList();
                ViewBag.GroupId = model.GroupId;
                return View(model);
            }

            var response = await _http.PostAsync(
                $"http://localhost:5009/api/usergroups/add?userId={model.SelectedUserId}&groupId={model.GroupId}",null);
                    

            if (!response.IsSuccessStatusCode)
            {   var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Failed to add user.");
                

                // repopulate users again
                var allUsers = await _http.GetFromJsonAsync<List<User>>("http://localhost:5009/api/users") ?? new List<User>();
                ViewBag.AllUsers = allUsers.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Username
                }).ToList();
                ViewBag.GroupId = model.GroupId;

                return View(model);
            }

            return RedirectToAction("Details", new { id = model.GroupId });
        }

        // POST: Remove User from Group
        [HttpPost]
        public async Task<IActionResult> RemoveMember(int userId, int groupId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/usergroups/remove")
            {
                Content = JsonContent.Create(new { userId, groupId })
            };
            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                TempData["Error"] = "Could not remove user.";

            return RedirectToAction("Details", new { id = groupId });
        }

        // GET: Groups/Create
        public IActionResult Create() => View();

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                var response = await _http.PostAsJsonAsync("api/group", group);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _http.GetFromJsonAsync<Group>($"api/group/{id}");
            if (group == null) return NotFound();
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group group)
        {
            if (id != group.Id) return BadRequest();

            var response = await _http.PutAsJsonAsync($"api/group/{id}", group);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _http.GetFromJsonAsync<Group>($"api/group/{id}");
            if (group == null) return NotFound();
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _http.DeleteAsync($"api/group/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
