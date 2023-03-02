using Melberg.Core.InfluxDB;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Contexts;

public class InfluxDBContext : DefaultContext
{
    public InfluxDBContext(IStandardInfluxDBClientFactory factory) : base(factory) { }
}