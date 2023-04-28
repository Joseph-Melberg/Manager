using MelbergFramework.Core.InfluxDB;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Contexts;

public class InfluxDBContext : DefaultContext
{
    public InfluxDBContext(IStandardInfluxDBClientFactory factory ) : base(factory) { }
}