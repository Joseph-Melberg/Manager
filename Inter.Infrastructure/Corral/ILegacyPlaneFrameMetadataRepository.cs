using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;
public interface ILegacyPlaneFrameMetadataRepository
{
    Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata model);
}