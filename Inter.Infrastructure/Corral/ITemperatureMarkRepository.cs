using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;

public interface ITemperatureMarkRepository
{
    Task RecordTemperature(TemperatureMark mark, CancellationToken ct);
}