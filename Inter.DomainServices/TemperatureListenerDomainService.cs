using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;
public class TemperatureListenerDomainService : ITemperatureListenerDomainService
{
    private readonly ITemperatureListenerInfrastructureService _infraservice;
    public TemperatureListenerDomainService(ITemperatureListenerInfrastructureService infrastructureService)
    {
        _infraservice = infrastructureService;
    }

    public Task RecordTempAsync(TemperatureMark[] marks) => Task.WhenAll(marks.Select(_ => _infraservice.InsertTemperatureAsync(_)).ToList()); 
}