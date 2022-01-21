using System;

namespace Inter.Infrastructure.MySQL.Models;
public class PlaneFrameMetadataModel
{
    public int id {get; set;}
    public string hostname {get; set;}
    public string antenna {get; set;}
    public int detailed {get; set;}
    public int total {get; set;}
    public DateTime mark {get; set;}
}