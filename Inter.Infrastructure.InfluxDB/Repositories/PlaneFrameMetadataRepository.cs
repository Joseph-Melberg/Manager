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

    public Task LogPlaneMetadata(PlaneFrameMetadata metadata) => Context.WritePointAsync(metadata.ToDataModel(),"plane_data", "Inter");
}