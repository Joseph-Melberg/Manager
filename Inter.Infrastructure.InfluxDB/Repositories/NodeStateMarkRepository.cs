using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class NodeStateMarkRepository : BaseInfluxDBRepository<InfluxDBContext>, INodeStateMarkRepository
{
    public NodeStateMarkRepository(InfluxDBContext context) : base(context) { }
    public Task MarkNodeStatusAsync(NodeStatus status) => Context.WritePointAsync(status.ToDataModel(),"node_data","Inter");
}