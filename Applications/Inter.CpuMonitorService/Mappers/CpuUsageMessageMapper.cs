using Inter.CpuMonitorService.Messages;
using Inter.Domain;

namespace Inter.CpuMonitorService.Mappers;

public static class CpuUsageMessageMapper
{
    public static CpuUtilization ToDomain(this CpuUsageMessage message)
    {
        if(message == null)
        {
            return null;
        }

        return new CpuUtilization 
        {
            Host = message.HostName,
            Utilization = message.Usage,
            TimeStamp = DateTime.UtcNow
        };
    }
}