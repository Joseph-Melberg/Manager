using Inter.Domain;

namespace Inter.Infrastructure.MySQL.Mappers;
public static class HeartbeatMapper
{
    public static Heartbeat ToDomain(this HeartbeatModel model)
    {
        if(model == null)
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

    public static HeartbeatModel ToModel(this Heartbeat domainModel)
    {
        if(domainModel == null)
        {
            return null;
        }

        return new HeartbeatModel
        {
            announced = domainModel.announced,
            mac = domainModel.mac,
            name = domainModel.name,
            online = domainModel.online,
            timestamp = domainModel.timestamp
        };
    }
}