using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class MetricMarkRepository : BaseInfluxDBRepository<InfluxDBContext>, IMetricMarkRepository
{
    public MetricMarkRepository(InfluxDBContext context) : base(context) { }
    public Task MarkMetricAsync(Metric metric) =>
        Context.WritePointAsync(metric.ToDataModel(),"service_data","Inter");
}