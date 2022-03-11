using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Mappers;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Repositories;

public class PlaneFrameMetadataRepository : BaseInfluxDBRepository<InfluxDBContext>, IPlaneFrameMetadataRepository
{
    public PlaneFrameMetadataRepository(InfluxDBContext context) : base(context) { }

    public async Task LogPlaneMetadata(PlaneFrameMetadata metadata)
    {
        var data = metadata.ToDataModel();
        await Context.WritePointAsync(data,"plane_data", "Inter");
    }
}