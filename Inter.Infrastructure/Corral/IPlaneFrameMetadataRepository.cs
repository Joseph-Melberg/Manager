using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IPlaneFrameMetadataRepository
    {
        Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata model);
    }
}