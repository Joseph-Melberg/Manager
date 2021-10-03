using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneApiInfrastructureService : IPlaneApiInfrastructureService
    {
        private readonly IPlaneCacheRepository _planeCacheRepository;
        public PlaneApiInfrastructureService(IPlaneCacheRepository planeCacheRepository)
        {
            _planeCacheRepository = planeCacheRepository;
        }


        public Task<PlaneFrame> GetFrameAsync(long time) => _planeCacheRepository.GetPlaneFrameAsync(time);
    }
}