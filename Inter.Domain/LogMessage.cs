using System;

namespace Inter.Domain
{
    public class LogMessage
    {
        public string Severity { get; set; }
        public Guid Title { get; set; }
        public string DeviceName { get; set; }
        public string ProcessName { get; set; }
        public string FormattedMessage { get; set; }
        public string MAC { get; set; }
    }
}
