using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;
public class TemperatureListenerService : ITemperatureListenerService
{
    private readonly ITemperatureListenerInfrastructureService _infraservice;
    public TemperatureListenerService(ITemperatureListenerInfrastructureService infrastructureService)
    {
        _infraservice = infrastructureService;
    }

    public async Task RecordTempAsync(TemperatureMark[] marks)
    {
        foreach(var mark in marks)
        {
            await _infraservice.InsertTemperatureAsync(mark);
        }

        await _infraservice.SaveRecordsAsync();
    }
}