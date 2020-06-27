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

        //String BASE_URL = "https://developer.nps.gov/api/v1/parks?parkCode=acad&api_key=2QOvudpqVXxRVoipbiY3VKhnRiBGtfz8b8aJwIfH";

        HttpClient httpClient;

        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "2QOvudpqVXxRVoipbiY3VKhnRiBGtfz8b8aJwIfH"; //API key 

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
      httpClient.DefaultRequestHeaders.Accept.Add(
          new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

      string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=20";
      string parksData = "";

      Parks parks = null;

      httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

      try
      {
        HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();

        if (response.IsSuccessStatusCode)
        {
          parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
          
        }

        if (!parksData.Equals(""))
        {
          // JsonConvert is part of the NewtonSoft.Json Nuget package
          parks = JsonConvert.DeserializeObject<Parks>(parksData);

          ViewBag.explaination = "Total parks received from Data.gov API: "+parks.total;
        }
      }
      catch (System.Exception e)
      {
        // This is a useful place to insert a breakpoint and observe the error message
        Console.WriteLine(e.Message);
      }

      return View(parks);
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
