using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IPlaneApiInfrastructureService
    {
        Task<PlaneFrame> GetFrameAsync(long time);
    }
}