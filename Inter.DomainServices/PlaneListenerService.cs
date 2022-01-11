using System;
using System.Linq;
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
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
                    var now = epoch.AddSeconds(frame.Now);
                    var detailed = frame.Planes.Count();
                    var total = detailed;
                    var metadata = new PlaneFrameMetadata
                    {
                        
                        Antenna = "aggregate",
                        Detailed = detailed,
                        Total = total, 
                        Hostname = "center3",
                        Timestamp = now   
                    };

                    await _infraservice.UploadPlaneFrameMetadataAsync(metadata);
                }
                catch(Exception e)
                {
                    //Something is wrong but I am not going to debug it yet
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}