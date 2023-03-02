using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class TemperatureMarkRepository : BaseInfluxDBRepository<InfluxDBContext>, ITemperatureMarkRepository
{
    public TemperatureMarkRepository(InfluxDBContext context) : base(context) { }

    public Task RecordTemperature(TemperatureMark mark, CancellationToken ct)
    {
        return Context.WritePointAsync(mark.ToDataModel(),"node_data","Inter");
    }
}
 