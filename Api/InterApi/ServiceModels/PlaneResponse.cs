using Newtonsoft.Json;

namespace InterApi.ServiceModels;

public class PlaneResponse 
{
    [JsonProperty("hexValue")] 
    public string HexValue {get; set;}
    [JsonProperty("squawk")]
    public string Squawk {get; set;}
    [JsonProperty("flight")]
    public string Flight {get; set;}
    [JsonProperty("lat")]
    public float? Latitude {get; set;}
    [JsonProperty("lon")]
    public float? Longitude {get; set;}
    [JsonProperty("nucp")]
    public string Nucp {get; set;}
    [JsonProperty("altitude")]
    public int? Altitude {get; set;}
    [JsonProperty("vert_rate")]
    public int? VerticleRate {get; set;}
    [JsonProperty("track")]
    public int? Track {get; set;}
    [JsonProperty("speed")]
    public int? Speed {get; set;}
    [JsonProperty("category")]
    public string Category {get; set;}
    [JsonProperty("messages")]
    public string Messages {get; set;}
    [JsonProperty("rssi")]
    public float? Rssi {get;set;}
}