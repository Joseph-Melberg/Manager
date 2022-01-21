using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;
public interface IPlaneRepository
{
    Task<int> PlaneCount(); 
    Task SaveChangesAsync();
    Task AddPlaneAsync(Plane plane,int now, DateTime time);

}