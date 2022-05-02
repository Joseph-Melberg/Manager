using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Microsoft.AspNetCore.Mvc;

namespace InterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaneController : ControllerBase
    {
        private IPlaneApiService _service;
        public PlaneController(IPlaneApiService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{source}/frame")]
        public async Task<PlaneFrame> GetFrameByDeviceAsync(string source, [FromQuery] string antenna,[FromQuery] long? time)
        { 
            if (time == null)
            {
                time = OffsetUtcNow;
            }

            return await _service.GetFrameByDeviceAsync(source,antenna,time.Value);
        }

        [HttpGet]
        [Route("frame")]
        public async Task<PlaneFrame> GetFrameAsync([FromQuery] long? time)
        {
            if (time == null)
            {
                time = OffsetUtcNow;
            }
            return await _service.GetFrameAsync(time.Value);
        }

        private Int32 OffsetUtcNow =>(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - 3;


    }
}
