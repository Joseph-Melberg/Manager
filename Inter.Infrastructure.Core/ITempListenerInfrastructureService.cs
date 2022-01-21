using Inter.Domain;
using System.Threading.Tasks;

namespace Inter.Infrastructure.Core;
public interface ITemperatureListenerInfrastructureService
{
    Task InsertTemperatureAsync(TemperatureMark mark);

    Task SaveRecordsAsync();
}