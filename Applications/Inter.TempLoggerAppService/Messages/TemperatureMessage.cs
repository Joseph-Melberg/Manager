using System;

namespace Inter.TempLoggerAppService.Messages
{
    public class TemperatureMessage
    {
        public string HostName {get;set;}

        public DateTime Timestamp {get; set;}
        
        public TemperatureDetail[] Temperatures {get; set;}
    }
}