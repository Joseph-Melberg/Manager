using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface INodeApiInfrastructureService
    {
        Task<List<Heartbeat>> GetStatiAsync();
    }
}
