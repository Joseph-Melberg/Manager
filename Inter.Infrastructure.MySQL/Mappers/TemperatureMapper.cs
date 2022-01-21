using Inter.Domain;
using Inter.Infrastructure.MySQL.Models;

namespace Inter.Infrastructure.MySQL.Mappers;
public static class TemperatureMapper
{
    public static TemperatureMarkModel ToModel(this TemperatureMark mark)
    {
        if(mark == null)
        {
            return null;
        }

        return new TemperatureMarkModel
        {
            hostname = mark.HostName,
            specific = mark.PartName,
            temperature = mark.Temperature,
            timestamp = mark.Timestamp
        };
    }
}