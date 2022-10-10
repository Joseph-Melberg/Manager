using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface INodeApiDomainService
{
    Task<int> GetUpCountAsync();

    Task<IList<Heartbeat>> GetListAsync();
}