using System;
using System.Diagnostics;
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
                Stopwatch timer = new Stopwatch();
                timer.Start();
                await Context.PlaneFrameMetadata.AddAsync(result );
                var timeToAdd = timer.ElapsedMilliseconds;
                await Context.SaveChangesAsync();
                var timeToSave = timer.ElapsedMilliseconds - timeToAdd;
                Context.ChangeTracker.Clear();

                timer.Stop();
                Console.WriteLine($"Add:{timeToAdd},Save:{timeToSave}");
            }
        }
    }
}