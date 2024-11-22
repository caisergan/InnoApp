using InnoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace InnoMvc.Controllers
{
    public class ProfileController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7288");
        private readonly HttpClient _httpClient;

        public ProfileController()
        {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("token");
            string username = HttpContext.Session.GetString("userName");
            ViewBag.Token = token;
            ViewBag.Username = username;
            return View();
        }

        public IActionResult ReturnProfile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            List<UserViewModel> userlist = new List<UserViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:7288/InnoApi/user").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userlist = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }


            UserViewModel user = userlist.FirstOrDefault(u => u.ID == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PutAsync($"https://localhost:7288/InnoApi/user/{model.ID}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "User updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update user.");
                }
            }

            return View(model);
        }

    }
}
