using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vueling.Api.Client.Models;
using Vueling.Data;
using Vueling.Data.Models;
using static Vueling.Data.Models.Response;

namespace Vueling.Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestController : ControllerBase
    {
        private readonly IOptions<AppSettings> settings;
        private readonly PassengerRepository repository;

        public ManifestController(IOptions<AppSettings> settings, PassengerRepository repository)
        {
            this.settings = settings;
            this.repository = repository;
        }

        //[HttpGet("{flight}")]
        [HttpGet]
        [Route("update/{flight}")]
        public ActionResult Get(string flight)
        {
            //var passengers = new List<Passenger>();
            //var _passengers = repository.updatePassengers(passengers);
            //return new JsonResult(passengers);

            Response response = new Response { Status = ResponseStatus.OK, Message = "OK" };
            doRequest(flight);
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("add")]
        [EnableCors("Cors")]
        [Authorize]
        public ActionResult Add(string flight)
        {
            Response response = new Response { Status = ResponseStatus.OK, Message = "OK" };
            doRequest(flight);
            //Response response = repository.addPassenger(passenger);
            return new JsonResult(response);
        }

        private void doRequest(string flight) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55909/api/manifest/get");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(flight).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Passenger>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    //Console.WriteLine("{0}", d.Name);
                }
            }
            else
            {
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();
        }

        private ObjectResult userUnauthorized()
        {
            return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid credentials!");
        }
    }
}