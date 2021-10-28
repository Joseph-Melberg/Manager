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

        public async Task HandleMessageAsync(PlaneFrame frame)
        {
            Console.WriteLine($"Processing frame {frame.Now}");
            if(frame.Planes.Length != 0)
            {
                try
                {

                    await _infraservice.AddPlaneFrameAsync(frame);
                }
                catch(Exception e)
                {
                    //Something is wrong but I am not going to debug it yet
                    //Console.WriteLine(e.Message);
                }
            }
        }
    }
}