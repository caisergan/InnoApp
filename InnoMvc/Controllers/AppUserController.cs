using InnoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace InnoMvc.Controllers
{
    public class AppUserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7288");
        private readonly HttpClient _httpClient;

        public AppUserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> userlist = new List<UserViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync("https://localhost:7288/InnoApi/user").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userlist = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(userlist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("https://localhost:7288/InnoApi/user", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "User Created Succesfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
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
        public IActionResult Edit(UserViewModel model)
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
        [HttpDelete]
        public IActionResult Delete(int id)
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync($"https://localhost:7288/InnoApi/user/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "User deleted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Failed to delete user.";
                return RedirectToAction("Index");
            }
        }
    }
}