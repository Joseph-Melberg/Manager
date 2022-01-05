using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Mappers;
using Melberg.Infrastructure.MySql;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class PlaneFrameMetadataRepository : BaseRepository<ReadWriteContext>, IPlaneFrameMetadataRepository
    {
        public PlaneFrameMetadataRepository(ReadWriteContext context) : base(context)
        {
        }

        public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata model)
        {
            var j = Context.Heartbeats.Select(_ => _).ToList();
            var k = Context.PlaneFrameMetadata.Select(_ => _).ToList();
            var result = model.ToModel();
            if(result != null)
            {
                await Context.PlaneFrameMetadata.AddAsync(result );
                await Context.SaveChangesAsync();
            }
        }
    }
}