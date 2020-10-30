using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class LogListenerInfrastructureService : ILogListenerInfrastructureService
    {
        private readonly ILogRepository _logRepository;
        public LogListenerInfrastructureService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public Task AddLog(LogModel logModel)
        {
            return _logRepository.AddLog(logModel);
        }
    }
}
