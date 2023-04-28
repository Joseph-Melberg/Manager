using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class CpuUtilizationMarkRepository : BaseInfluxDBRepository<InfluxDBContext>, ICpuUtilizationMarkRepository
{
    public CpuUtilizationMarkRepository(InfluxDBContext context) : base(context) { }

    public Task RecordUsageAsync(CpuUtilization usage, CancellationToken ct)
    {
        return Context.WritePointAsync(usage.ToDataModel(),"node_data","Inter");
    }
}
 
