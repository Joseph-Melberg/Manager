using System;

namespace Inter.Domain
{
    public class TemperatureMark
    {
        public string HostName {get; set;}

        public string PartName {get; set;}
        public DateTime Timestamp {get; set;}

        public double Temperature {get; set;}
    }
}