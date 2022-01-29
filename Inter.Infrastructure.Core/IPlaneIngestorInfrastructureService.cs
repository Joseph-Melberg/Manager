using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface IPlaneIngestorInfrastructureService
{
    Task IngestPlaneFrameAsync(PlaneFrame planeFrame);
}