using System.Threading.Tasks;
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
        [Route("count")]
        public async Task<int> GetDetailCount()
        {
            var result = await _service.CountDetailed();
            return result;
        }
    }
}