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
        [Route("frame")]
        public async Task<PlaneFrame> GetFrameAsync()
        {
            return await _service.GetFrameAsync(DateTime.UtcNow.ToFileTimeUtc() -1);
        }
        
        [HttpGet]
        [Route("frame/{time}")]
        public async Task<PlaneFrame> GetFrameAsync(long time)
        {
            return await _service.GetFrameAsync(time);
        }
    }
}