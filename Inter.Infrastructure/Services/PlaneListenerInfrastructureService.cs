using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneListenerInfrastructureService : IPlaneListenerInfrastructureService
    {
        private readonly IPlaneCacheRepository _planeCacheRepository;
        
        public PlaneListenerInfrastructureService(IPlaneCacheRepository planeCacheRepository)
        {
            _planeCacheRepository = planeCacheRepository;
        }

        public async Task AddPlaneFrameAsync(PlaneFrame frame)
        {
            await _planeCacheRepository.InsertPlaneFrameAsync(frame);
        }
    }
}