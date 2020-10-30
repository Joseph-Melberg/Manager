using System;

namespace Inter.Domain
{
    public class LogModel
    {
        public int LogID;
        public string Severity;
        public Guid Title;
        public DateTime Timestamp;
        public string DeviceName;
        public string ProcessName;
        public string FormattedMessage;
        public string MAC;
    }
}