using System;
using System.Threading.Tasks;

namespace Inter.Infrastructure.Core
{
    public interface IRemoteInfrastructureService
    {
        Task<bool> Register(string name);
    }
}
