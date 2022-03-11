using Melberg.Core.InfluxDB;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Contexts;

public class InfluxDBContext : DefaultContext
{
    public InfluxDBContext(IInfluxDBConfigurationProvider configurationProvider) : base(configurationProvider) { }
}