using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneApiInfrastructureService : IPlaneApiInfrastructureService
    {
        private readonly IPlaneFrameRepository _planeFrameRepo;
        public PlaneApiInfrastructureService(IPlaneFrameRepository planeFrameRepository)
        {
            _planeFrameRepo = planeFrameRepository;
        }

        public async Task<PlaneFrame> GetFrameAsync(long time)
        {
            return await _planeFrameRepo.GetFrameAsync(time);
        }
    }
}