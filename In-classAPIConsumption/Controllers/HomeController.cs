using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using In_classAPIConsumption.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace In_classAPIConsumption.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        String BASE_URL = "https://developer.nps.gov/api/v1/parks?parkCode=acad&api_key=2QOvudpqVXxRVoipbiY3VKhnRiBGtfz8b8aJwIfH";
        HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = httpClient.GetAsync(BASE_URL).GetAwaiter().GetResult();

            if(response.IsSuccessStatusCode)
            {
                ViewBag.explaination = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

  
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

/**        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
       public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } */
    } 
}
