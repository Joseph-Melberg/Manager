using System;
namespace OnlineOfflineReaderService.Domain
{
    public class HeartBeatModel
    {
        public string name { get; set; }
        public string mac { get; set; }
        public DateTime timestamp { get; set; }
    }
}
