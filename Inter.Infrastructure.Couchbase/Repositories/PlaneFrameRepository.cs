using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Melberg.Infrastructure.Couchbase;

namespace Inter.Infrastructure.Couchbase
{
    public class PlaneFrameRepository : CouchRepository, IPlaneFrameRepository
    {
        public PlaneFrameRepository(ICouchClientFactory couchClientFactory) : base(couchClientFactory, "plane_records")
        {
        }

        public async Task InsertFrameAsync(PlaneFrame frame)
        {
            await Collection.UpsertAsync($"{frame.Now}",frame);
        }

        public async Task<PlaneFrame> GetFrameAsync( long time)
        {
            try
            {
                var result = await Collection.GetAsync($"{time}");

                return result.ContentAs<PlaneFrame>();
            }
            catch (System.Exception)
            {
                return new PlaneFrame
                {
                    Now = (int)time,
                    Planes = new Plane[0]
                }   ; 
            
            }
        }
    }
}