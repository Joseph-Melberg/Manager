using System;
namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IOnlineOfflineReaderInfrastructureService
    {
        void Update(string Name, DateTime timestamp);
    }
}
