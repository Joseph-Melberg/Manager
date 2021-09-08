using System;

namespace Inter.Infrastructure.MySQL.Models
{
    public class TemperatureMarkModel
    {
        public int id {get; set;}
        public string hostname {get; set;}
        public string specific {get; set;}
        public DateTime timestamp {get; set;}
        public double temperature {get; set;}
    }
}