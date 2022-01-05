using Inter.Domain;
using Inter.Infrastructure.MySQL.Models;

namespace Inter.Infrastructure.MySQL.Mappers
{
    public static class PlaneFrameMetadataMapper
    {
        public static PlaneFrameMetadataModel ToModel(this PlaneFrameMetadata domain)
        {
            if(domain == null)
            {
                return null;
            }

            return new PlaneFrameMetadataModel
            {
                antenna = domain.Antenna,
                detailed = domain.Detailed,
                hostname = domain.Hostname,
                total = domain.Total,
                mark = domain.Timestamp
            };
        }
    }
}