using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using InterApi.Mappers;
using InterApi.ServiceModels;
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
        [Route("frame")]
        public async Task<PlaneFrameResponse> GetFrameAsync([FromQuery] long? time)
        {
            if (time == null)
            {
                time = OffsetUtcNow;
            }
            return (await _service.GetFrameAsync(time.Value)).ToResponse();
        }

        private Int32 OffsetUtcNow =>(Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - 3;


    }
}
