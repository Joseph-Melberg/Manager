using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneListenerInfrastructureService : IPlaneListenerInfrastructureService
    {
        private readonly IPlaneFrameRepository _planeFrameRepository;
        
        public PlaneListenerInfrastructureService(IPlaneFrameRepository planeFrameRepository)
        {
            _planeFrameRepository = planeFrameRepository;
        }

        public async Task AddPlaneFrameAsync(PlaneFrame frame)
        {
            await _planeFrameRepository.InsertFrameAsync(frame);
        }
    }
}