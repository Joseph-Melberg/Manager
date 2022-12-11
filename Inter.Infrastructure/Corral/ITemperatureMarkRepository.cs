using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
namespace Inter.Infrastructure.Corral;

public interface ITemperatureMarkRepository
{
	public Task MarkTemperature(TemperatureMark mark);
}

