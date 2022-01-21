using System;

namespace Inter.Domain;
public class LogModel
{
    public int LogID { get; set; }
    public string Severity { get; set; }
    public string Title { get; set; }
    public DateTime Timestamp { get; set; }
    public string DeviceName { get; set; }
    public string ProcessName { get; set; }
    public string FormattedMessage { get; set; }
    public string MAC { get; set; }
}