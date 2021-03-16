using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IPlaneListenerInfrastructureService
    {
       Task AddPlaneAsync(Plane plane, int now, DateTime time);

       Task SaveChangesAsyc();
    }
}