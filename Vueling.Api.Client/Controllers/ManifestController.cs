using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [HttpGet]
        [AllowAnonymous]
        [EnableCors("Cors")]
        [Route("update/{flight}")]
        public ActionResult Get(string flight)
        {
            Response response = new Response { Status = ResponseStatus.KO, Message = "There was a problem contacting manifest Api." };
            List<Passenger> passengers =  doRequest(flight);
            if (passengers != null) response = processManifest(passengers);

            return new JsonResult(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [EnableCors("Cors")]
        [Route("search")]
        public ActionResult Post(Passenger passenger) //string name = "", string surname = "", string seat = ""
        {
            List<Passenger> passengers = repository.getPassengers(passenger.Name, passenger.Surname, passenger.Seat);
            return new JsonResult(passengers);
        }

        private List<Passenger> doRequest(string flight) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(settings.Value.ApiUrl);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            List<Passenger> passengers = null;
            HttpResponseMessage response = client.GetAsync(flight).Result;
            if (response.IsSuccessStatusCode)
            {
                passengers = (List<Passenger>)response.Content.ReadAsAsync<IEnumerable<Passenger>>().Result;
            }
            client.Dispose();

            return passengers;
        }

        private Response processManifest(List<Passenger> passengers) {

            return repository.updatePassengers(passengers);
        }
    }
}