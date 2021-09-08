using System.Linq;
using Inter.Domain;
using Inter.TempLoggerAppService.Messages;

namespace Inter.TempLoggerAppService.Mappers
{
    public static class TemperatureMessageMapper
    {
        public static TemperatureMark[] ToDomain(this TemperatureMessage message)
        {
            if(message == null)
            {
                return null;
            }
            return message?.Temperatures.Select(_ => new TemperatureMark{HostName = message.HostName, Timestamp = message.Timestamp,PartName = _.PartName, Temperature = _.Temperature}).ToArray();
        }
    }
}