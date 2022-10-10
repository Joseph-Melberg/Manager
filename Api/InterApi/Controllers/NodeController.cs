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
        private INodeApiDomainService _service;
        public NodeController( INodeApiDomainService service)
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
        [Route("list")]
        public async Task<IList<Heartbeat>> GetListAsync() => await _service.GetListAsync();
    }
}
