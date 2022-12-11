using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class TemperatureListenerInfrastructureService : ITemperatureListenerInfrastructureService
{
    private ITemperatureMarkRepository _temperatureRepository;
    public TemperatureListenerInfrastructureService(ITemperatureMarkRepository temperatureRepository)
    {
        _temperatureRepository = temperatureRepository;
    } 
    public Task InsertTemperatureAsync(TemperatureMark mark) =>
	    _temperatureRepository.MarkTemperature(mark);
}
