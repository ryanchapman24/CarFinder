using CarFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarFinder.Controllers
{

    /// <summary>
    /// Get car information
    /// </summary>
    [RoutePrefix("api/Cars")]
    public class CarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private Entities db2 = new Entities();

        /// <summary>
        /// Get all distinct years that appear in car database
        /// </summary>
        /// <returns></returns>
        [Route("Year")]
        public async Task<List<string>> GetYears()
        {
            return await db.GetYears();
        }

        //[Route("Year")]
        //public virtual List<string> GetYears()
        //{
        //    return db2.UniqueCarYears().ToList();
        //}

        /// <summary>
        /// Get all distinct makes for given year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Route("Make")]
        public async Task<List<string>> GetMakes(string year)
        {
            return await db.GetMakes(year);
        }

        /// <summary>
        /// Give all distinct car models for given year and make
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("Model")]
        public async Task<List<string>> GetModels(string year, string make)
        {
            return await db.GetModels(year, make);
        }

        /// <summary>
        /// Get all distinct car trims for given year and make and model
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Trim")]
        public async Task<List<string>> GetTrims(string year, string make, string model)
        {
            return await db.GetTrims(year, make, model);
        }

        /// <summary>
        /// Get car object given year and make and model and trim
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        [Route("Car")]
        public async Task<Car> GetCar(string year, string make, string model, string trim)
        {
            return await db.GetCar(year, make, model, trim);
        }

        /// <summary>
        /// Get recall data for car given year and make and model
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("CarData")]
        public async Task<IHttpActionResult> GetCarData(string year, string make, string model)
        {
            HttpResponseMessage response;
            var car = new carViewModel();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://one.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/"
                        + year + "/make/" + make + "/model/" + model + "?format=json");
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Recall>(json);
                    car.RecallResults = result.Results;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }

            return Ok(car);
        }

        /// <summary>
        /// Gets the car image given year and make and model and trim
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        [Route("CarImage")]
        public async Task<IHttpActionResult> getCarImage(string year, string make, string model, string trim)
        {
            var car = new carViewModel();

            //Bing Cognitive Search
            var query = year + " " + make + " " + model + " " + trim;
            var url = $"https://api.cognitive.microsoft.com/bing/v7.0/images/" +
                $"search?q={WebUtility.UrlEncode(query)}" +
                $"&count=20&offset=0&mkt=en-us&safeSearch=Strict";
            var requestHeaderKey = "Ocp-Apim-Subscription-Key";
            var requestHeaderValue = ConfigurationManager.AppSettings["bingSearchKey"];

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeaderKey, requestHeaderValue);
                    var json = await client.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<SearchResults>(json);
                    car.ImageUrl = result.Images.Select(i => i.ContentUrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return Ok(car);
        }
    }
}
