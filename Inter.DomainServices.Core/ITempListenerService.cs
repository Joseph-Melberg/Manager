using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core
{
    public interface ITemperatureListenerService
    {
        Task RecordTempAsync(TemperatureMark[] marks);
    }
}