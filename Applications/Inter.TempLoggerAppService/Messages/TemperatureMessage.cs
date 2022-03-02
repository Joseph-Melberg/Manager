using System;
using Melberg.Infrastructure.Rabbit.Messages;

namespace Inter.TempLoggerAppService.Messages;
public class TemperatureMessage : StandardMessage 
{
    public string HostName {get;set;}

    public DateTime Timestamp {get; set;}
    
    public TemperatureDetail[] Temperatures {get; set;}

    public override string GetRoutingKey() => "temperature.placeholder";
}