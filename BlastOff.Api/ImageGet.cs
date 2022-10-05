using BlastOff.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;



using System.Collections.Generic;
using System.Linq;

using System.Net.Http.Json;



namespace BlastOff.Api
{
    public class ImageGet
    {
        private static string GetRandomDate()
        {
            var random = new Random();
            var startDate = new DateTime(1995, 06, 16);
            var range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(random.Next(range)).ToString("yyyy-MM-dd");
        }

        [FunctionName("ImageGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "image")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Executing ImageOfDayGet.");
            var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apiKey}&hd=true&date={GetRandomDate()}");
            var result = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(JsonConvert.DeserializeObject(result));
        }
    }
}