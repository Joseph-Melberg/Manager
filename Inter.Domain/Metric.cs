using System;

namespace Inter.Domain;

public class Metric
{
    public long TimeInMS { get; set; }
    public string Application { get; set; }
    public DateTime TimeStamp { get; set; }
}