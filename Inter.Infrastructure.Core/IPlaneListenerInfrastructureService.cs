using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface IPlaneListenerInfrastructureService
{
   Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata);
   Task AddPlaneFrameAsync(PlaneFrame frame);
}