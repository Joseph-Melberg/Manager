using Inter.Domain;
using Inter.Infrastructure.MySQL;

namespace Inter.Infrastructure.Mappers
{
    public static class HeatbeatModelMapper
    {
        public static Heartbeat ToDomain(this HeartbeatModel model)
        {
            if(model is null)
            {
                return null;
            }
            return new Heartbeat
            {
                announced = model.announced,
                mac = model.mac,
                name = model.name,
                online = model.online,
                timestamp = model.timestamp
            };
        }

        public static HeartbeatModel ToModel(this Heartbeat model)
        {
            if(model is null)
            {
                return null;
            }

            return new HeartbeatModel
            {
                announced = model.announced,
                mac = model.mac,
                name = model.name,
                online = model.online,
                timestamp = model.timestamp
            };
        }
    }
}