using Melberg.Infrastructure.InfluxDB;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using Inter.Infrastructure.Corral;
using Inter.Domain;
using System.Threading;
using System.Threading.Tasks;
namespace Inter.Infrastructure.InfluxDB.Repositories;

public class TemperatureMarkRepository :
	BaseInfluxDBRepository<InfluxDBContext>,
	ITemperatureMarkRepository
{
    public TemperatureMarkRepository(InfluxDBContext context) : base(context) {}

    public Task MarkTemperature(TemperatureMark mark) =>
        Context.WritePointAsync(mark.ToDataModel(),"node_data","Inter");
}
