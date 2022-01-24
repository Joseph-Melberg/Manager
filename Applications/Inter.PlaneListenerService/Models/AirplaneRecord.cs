using Newtonsoft.Json;
namespace Inter.PlaneListenerService.Models;
public class AirplaneRecord
{
    [JsonProperty("now")]
    public double Now {get; set;}
    [JsonProperty("aircraft")]
    public AirplaneData[] Planes {get; set;}
    [JsonProperty("source")]
    public string Source {get; set;}
    [JsonProperty("antenna")]
    public string Antenna {get; set;}
}