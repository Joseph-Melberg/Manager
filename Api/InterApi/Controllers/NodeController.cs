using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Microsoft.AspNetCore.Mvc;


namespace InterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
        private INodeStatusService _service;
        public NodeController( INodeStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("count")]
        public async Task<int> GetCountAsync()
        {
            return await _service.GetUpCount();
        }
    }
}
