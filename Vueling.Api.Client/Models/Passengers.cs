using System;
using System.Collections.Generic;

namespace Vueling.Api.Client.Models
{
    public partial class Passengers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Seat { get; set; }
        public string Flight { get; set; }
    }
}
