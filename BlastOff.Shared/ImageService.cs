using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Transactions;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text.Json.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace BlastOff.Shared
{
    public interface IImageService
    {
        public Task<APODImage> GetImage(string apiKey);
        public Task<IEnumerable<APODImage>> GetImages(int days, string apiKey);

    }
    public class ImageService : IImageService
    {

        public ImageService()
        {
        }

        public async Task<APODImage> GetImage(string apiKey)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apiKey}&hd=true&date={GetRandomDate()}");
                var result = await response.Content.ReadAsStringAsync();
                //var result = await response.Content.ReadFromJsonAsync<APODImage>();


                JObject json = JObject.Parse(result);

                Console.WriteLine(json.GetType());
                Console.WriteLine(json);

                APODImage image = JsonConvert.DeserializeObject<APODImage>(json.ToString());

                //Console.WriteLine(image);

                return image;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

            return default;
        }

        public async Task<IEnumerable<APODImage>> GetImages(int count, string apiKey)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var response = await httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={apiKey}&hd=true&count={count}");
                var result = await response.Content.ReadAsStringAsync();

                // JObject json = JObject.Parse(result);

                //Console.WriteLine(json.GetType());
                //Console.WriteLine(json);

                // IEnumerable<APODImage> images = JsonConvert.DeserializeObject<IEnumerable<APODImage>>(json.ToString());
                IEnumerable<APODImage> images = JsonConvert.DeserializeObject<IEnumerable<APODImage>>(result.ToString());

                Console.WriteLine(images.GetType());
                Console.WriteLine(images);

                return images.OrderByDescending(img => img.Date);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }

            return default;
        }




        private static string GetRandomDate()
        {
            var random = new Random();
            var startDate = new DateTime(1995, 06, 16);
            var range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(random.Next(range)).ToString("yyyy-MM-dd");
        }

    }
}
