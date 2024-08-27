using InnoMvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.Reflection;
using System.Text;

namespace InnoMvc.Controllers
{
    public class AuthController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7288");
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.SignOutAsync();

            // Redirect to the login page or home page after logout
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(AppUserViewModel appUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appUserViewModel);
            }
            string data = JsonConvert.SerializeObject(appUserViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7288/api/account/login", content);
            if (response.IsSuccessStatusCode)
            {
				string responseBody = await response.Content.ReadAsStringAsync();
				dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
				string token = jsonResponse.token;
                string username = jsonResponse.userName;

                HttpContext.Session.SetString("token", token);
                HttpContext.Session.SetString("userName", username);


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View(appUserViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(AppUserViewModel appUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appUserViewModel);
            }
            try
            {
                string data = JsonConvert.SerializeObject(appUserViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _httpClient.PostAsync("https://localhost:7288/api/account/register", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);
                    string token = jsonResponse.token;
                    string username = jsonResponse.username;    
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("username", username);


                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }

    }
}
