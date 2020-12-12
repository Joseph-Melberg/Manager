using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class LogRepository : BaseRepository<LogContext>, ILogRepository
    {
        public LogRepository(LogContext context) : base(context)
        {
        }

        public async Task AddLog(LogModel log)
        {
            await Context.log.AddAsync(log);
            await Context.Save();
        }
    }
}