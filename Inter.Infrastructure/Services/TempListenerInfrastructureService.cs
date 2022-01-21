using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class TemperatureListenerInfrastructureService : ITemperatureListenerInfrastructureService
{
    private ITemperatureRepository _temperatureRepository;
    public TemperatureListenerInfrastructureService(ITemperatureRepository temperatureRepository)
    {
        _temperatureRepository = temperatureRepository;
    } 
    public async Task InsertTemperatureAsync(TemperatureMark mark) => await _temperatureRepository.RecordTemperatureAsync(mark);

    public async Task SaveRecordsAsync() => await _temperatureRepository.SaveRecordsAsync();
}