namespace Inter.PlaneIngestorService.Models;
public class AirplaneData
{
    public int? id {get; set;}
    public string? hex {get; set;}
    public string? squawk {get; set;}
    public ulong? squawk_update {get; set;}
    public string? flight {get;set;}
    public ulong? flight_update {get; set;}
    public float? lat {get; set;}
    public float? lon {get; set;}
    public string? nucp {get; set;}
    public ulong? position_update {get; set;}
    public int? altitude {get; set;}
    public ulong? altitude_update {get; set;}
    public int? vert_rate {get; set;}
    public ulong? vert_update {get; set;}
    public int? track {get; set;}
    public ulong? track_update {get; set;}
    public int? speed {get; set;}
    public ulong? speed_update {get; set;}
    public string? category {get; set;}
    public ulong? category_update {get; set;}
    public string? messages {get; set;}
    public float? rssi {get; set;}
}