using System;
namespace Inter.Domain
{
    public class LogMessage
    {
        public string Severity;
        public Guid Title;
        public string DeviceName;
        public string ProcessName;
        public string FormattedMessage;
        public string MAC;
    }
}
