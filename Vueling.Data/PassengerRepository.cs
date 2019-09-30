using System;
using System.Collections.Generic;
using System.Linq;
using Vueling.Data.Models;
using static Vueling.Data.Models.Response;

namespace Vueling.Data
{
    public class PassengerRepository
    {
        public Context context;

        public PassengerRepository(Context context) {
            this.context = context;
        }

        /// <summary>
        /// Returns a list of passengers of flight <flight>
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public List<Passenger> getPassengers(string flight)
        {
            return context.Passengers.ToList().Where(x => x.Flight == flight).ToList();
        }

        /// <summary>
        /// Updates passengers manifest
        /// </summary>
        /// <param name="passengers"></param>
        /// <returns></returns>
        public Response updatePassengers(List<Passenger> passengers)
        {
            string result = ResponseStatus.OK;
            string message = "Manifest updated correctly.";
            try
            {
                int newPassengers = 0;
                foreach (Passenger passenger in passengers) {
                    bool idExists = context.Passengers.Where(x => x.Id == passenger.Id).FirstOrDefault() != null;
                    if (!idExists) {
                        context.Passengers.Add(passenger);
                        newPassengers++;
                    }
                }
                context.SaveChanges();
                message = message + " " + newPassengers + " new passenger(s) added";
            }
            catch (Exception)
            {
                result = ResponseStatus.KO;
                message = "There was a problem updating manifest.";
            }
            return new Response { Status = result, Message = message };
        }

        public List<Passenger> getPassengers(string name, string surname, string seat) {

            List<Passenger> passengers = new List<Passenger>();
            if (name != string.Empty) passengers = context.Passengers.Where(x => x.Name == name).ToList();
            if (surname != string.Empty) passengers = context.Passengers.Where(x => x.Surname == surname).ToList();
            if (seat != string.Empty) passengers = context.Passengers.Where(x => x.Seat == seat).ToList();

            return passengers;
        }
    }
}
