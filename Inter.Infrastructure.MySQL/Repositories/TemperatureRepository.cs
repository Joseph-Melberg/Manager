using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Mappers;
using MelbergFramework.Infrastructure.MySql;

namespace Inter.Infrastructure.MySQL.Repositories;
public class TemperatureRepository : BaseRepository<TemperatureContext>, ITemperatureRepository
{
    public TemperatureRepository(TemperatureContext context) : base(context) {}

    public async Task RecordTemperatureAsync(TemperatureMark mark) => await Context.Temperature.AddAsync(mark.ToModel());

    public async Task SaveRecordsAsync() => await Context.SaveAsync();
}