using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;

namespace Inter.Infrastructure.MySQL.Repositories
{
    public class LogRepository: BaseRepository<ILogContext>, ILogRepository
    {
        public LogRepository(ILogContext context) : base(context)
        {
        }

        public Task AddLog(LogModel logModel)
        {
            _context.Log.AddAsync(logModel);
            return _context.Save();
        }
    }
}
