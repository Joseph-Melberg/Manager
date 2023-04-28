using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Inter.CpuMonitorService.Messages;

public class CpuUsageMessage : StandardMessage
{
    public string HostName {get; set;}
    public float Usage {get; set;}
    public override string GetRoutingKey() => "node.usage";
}