namespace Inter.PlaneIngestorService.Models;
public class AirplaneData
{
    public int? id {get; set;}
    public string? hex {get; set;}
    public string? squawk {get; set;}
    public string? flight {get;set;}
    public float? lat {get; set;}
    public float? lon {get; set;}
    public string? nucp {get; set;}
    public int? altitude {get; set;}
    public int? vert_rate {get; set;}
    public int? track {get; set;}
    public int? speed {get; set;}
    public string? category {get; set;}
    public string? messages {get; set;}
    public float? rssi {get; set;}
}