using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface ITemperatureListenerDomainService
{
    Task RecordTempAsync(TemperatureMark[] marks);
}