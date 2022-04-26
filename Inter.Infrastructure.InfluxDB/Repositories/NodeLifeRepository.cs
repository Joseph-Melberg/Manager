using System.Threading.Tasks;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class NodeLifeRepository : BaseInfluxDBRepository<InfluxDBContext>, INodeLifeRepository
{
    public NodeLifeRepository(InfluxDBContext context) : base(context) { }

    public async Task MarkStatusAsync(string nodeName, bool isAlive)
    {
        var mark = NodeLifeMarkerMapper.GenerateLifeMarker(nodeName, isAlive);

        await Context.WritePointAsync(mark,"plane_data", "Inter");
    }
}