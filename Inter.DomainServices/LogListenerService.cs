using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;

namespace Inter.DomainServices
{
    public class LogListenerService : ILogListenerService
    {
        public LogListenerService()
        {
        }

        public Task Process(LogMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
