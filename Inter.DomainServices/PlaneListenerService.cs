using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class PlaneListenerService : IPlaneListenerService
    {
        private readonly IPlaneListenerInfrastructureService _infraservice;
        public PlaneListenerService(IPlaneListenerInfrastructureService infraservice)
        {
            _infraservice = infraservice;
        }

        public async Task HandleMessageAsync(PlaneFrame Frame)
        {
            DateTime time = DateTime.UtcNow;
            foreach(var plane in Frame.Planes)
            {
                await _infraservice.AddPlaneAsync(plane, Frame.Now, time);
            }
            await _infraservice.SaveChangesAsyc();
        }
    }
}