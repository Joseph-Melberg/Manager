using System;

namespace Inter.Domain;

public class CpuUtilization 
{
    public string Host {get; set;}
    public DateTime TimeStamp {get; set;}
    public float Utilization {get; set;}
}