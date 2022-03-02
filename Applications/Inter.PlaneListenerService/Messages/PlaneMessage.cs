using Inter.PlaneListenerService.Models;
using Melberg.Infrastructure.Rabbit.Messages;
using Newtonsoft.Json;

namespace Inter.PlaneListenerService.Messages;
public class PlaneMessage : StandardMessage
{
    
    [JsonProperty("now")]
    public double Now {get; set;}
    [JsonProperty("aircraft")]
    public AirplaneData[] Planes {get; set;}
    [JsonProperty("source")]
    public string Source {get; set;}
    [JsonProperty("antenna")]
    public string Antenna {get; set;}
    public override string GetRoutingKey() => "plane.final";
}