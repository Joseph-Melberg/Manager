using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface ITemperatureRepository
    {
        Task RecordTemperatureAsync(TemperatureMark mark);

        Task SaveRecordsAsync();
    }
}