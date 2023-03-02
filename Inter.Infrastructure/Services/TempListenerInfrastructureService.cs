using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;

namespace Inter.Infrastructure.Services;
public class TemperatureListenerInfrastructureService : ITemperatureListenerInfrastructureService
{
    private readonly ITemperatureMarkRepository _temperatureMarkRepository;
    public TemperatureListenerInfrastructureService(ITemperatureMarkRepository temperatureMarkRepository)
    {
        _temperatureMarkRepository = temperatureMarkRepository;
    } 
    public async Task InsertTemperatureAsync(TemperatureMark mark) => await _temperatureMarkRepository.RecordTemperature(mark, System.Threading.CancellationToken.None);
}