using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneListenerInfrastructureService
    {
        private readonly IPlaneRepository _planeRepository;
        
        public PlaneListenerInfrastructureService(IPlaneRepository planeRepository)
        {
            _planeRepository = planeRepository;
        }

        public async Task AddPlaneAsync(Plane plane, int now, DateTime time)
        {
            await _planeRepository.AddPlaneAsync(plane,now, time);
        }

        public async Task SaveChangesAsync()
        {
            await _planeRepository.SaveChangesAsync();
        }
    }
}