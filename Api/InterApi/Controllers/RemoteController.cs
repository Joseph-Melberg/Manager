using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemoteController : ControllerBase
    {
        public RemoteController()
        {

        }

        [HttpGet]
        // GET: /<controller>/
        //[FromBody] string name
        public string Register([FromBody] JObject body)
        {
            string name = body["Name"].Value<string>();
            int port = body["Port"].Value<int>();

            return name;
        }
    }
}
