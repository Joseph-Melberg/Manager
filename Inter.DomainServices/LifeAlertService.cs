using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Microsoft.Extensions.Configuration;

namespace Inter.DomainServices
{
    public class LifeAlertService : ILifeAlertService
    {
        private readonly ILifeAlertInfrastructureService _infra;
        private readonly IConfiguration _configuration;
        public LifeAlertService(ILifeAlertInfrastructureService infrastructureService, IConfiguration configuration)
        {
            _infra = infrastructureService;
            _configuration = configuration;
        }

        public void Do()
        {
            var b = _configuration["Test"];
            _infra.SendMessage("6302478698@txt.att.net", "Report", "Info will go here");
        }
    }
}
