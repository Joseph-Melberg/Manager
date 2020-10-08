using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface ILifeAlertInfrastructureService
    {
        Task UpdateNode(HeartbeatModel model);
        HeartbeatModel[] GetStatuses();
        void SendMessage(string recipient, string subject, string message);
    }
}
