using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Mappers;
using Melberg.Infrastructure.MySql;

namespace Inter.Infrastructure.MySQL.Repositories;
public class PlaneFrameMetadataRepository : BaseRepository<ReadWriteContext>, ILegacyPlaneFrameMetadataRepository
{
    private readonly ReadWriteContext _context;
    public PlaneFrameMetadataRepository(ReadWriteContext context) : base(context) 
    {
        _context = context;
    }

    public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata model)
    {
        var result = model.ToModel();
        if(result != null)
        {
            await Context.PlaneFrameMetadata.AddAsync(result );
            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }
    }
}