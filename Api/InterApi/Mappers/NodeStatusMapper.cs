using Inter.Domain;
using InterApi.ServiceModels;

namespace InterApi.Mappers
{
    public static class NodeStatusMapper
    {
        public static NodeStatusResponse ToServiceModelLowDetail(this Heartbeat heartbeat)
        {
            if(heartbeat == null)
            {
                return null;
            }

            return new NodeStatusResponse
            {
                LastHeartbeat = heartbeat.timestamp,
                Name = heartbeat.name,
                Online = heartbeat.online
            };
        }

        public static NodeStatusDetailed ToServiceModelHighDetail(this Heartbeat heartbeat)
        {
            if(heartbeat == null)
            {
                return null;
            }

            return new NodeStatusDetailed
            {
                LastHeartbeat = heartbeat.timestamp,
                Mac = heartbeat.mac,
                Name = heartbeat.name,
                Online = heartbeat.online
            };
        }
    }
}