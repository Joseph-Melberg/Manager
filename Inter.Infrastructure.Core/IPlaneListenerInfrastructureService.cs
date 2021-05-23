using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IPlaneListenerInfrastructureService
    {
       Task AddPlaneFrameAsync(PlaneFrame frame);
    }
}