using System;
namespace Inter.Infrastructure.Core
{
    public interface ILifeAlertInfrastructureService
    {
        void SendMessage(string recipient, string subject, string message);
    }
}
