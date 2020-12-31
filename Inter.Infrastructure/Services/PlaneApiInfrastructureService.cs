using System.Threading.Tasks;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneApiInfrastructureService : IPlaneApiInfrastructureService
    {
        private readonly IPlaneRepository _planeRepo;
        public PlaneApiInfrastructureService(IPlaneRepository planeRepository)
        {
            _planeRepo = planeRepository;
        }

        public async Task<int> GetPlaneCount()
        {
            var result = await _planeRepo.PlaneCount();
            return result;
        }
    }
}