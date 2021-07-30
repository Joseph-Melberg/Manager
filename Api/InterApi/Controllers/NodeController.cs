using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Microsoft.AspNetCore.Mvc;


namespace InterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
        private INodeApiService _service;
        public NodeController( INodeApiService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("count")]
        public async Task<int> GetCountAsync()
        {
            return await _service.GetUpCountAsync();
        }

        [HttpGet]
        [Route("detail")]
        public async Task<IList<Heartbeat>> GetDetailsAsync() => await _service.GetStatiAsync();
    }
}
