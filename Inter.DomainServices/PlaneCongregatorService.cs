using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;
using Inter.Domain;
using System;

namespace Inter.DomainServices;

public class PlaneCongregatorService : IPlaneCongregatorService
{
    private readonly IPlaneCongregatorInfrastructureService _infrastructure;
    public PlaneCongregatorService(IPlaneCongregatorInfrastructureService infrastructure)
    {
        _infrastructure = infrastructure;
    }

    public async Task CongregatePlaneInfoAsync(long timestamp)
    {

        var offsetTimestamp = timestamp - 1; // look at the previous previous second
        var planeDictionary = new Dictionary<string,Plane>();
        await foreach(var frame in _infrastructure.CollectFramesAsync(offsetTimestamp))
        {
            //record info
            //combine
            foreach(var plane in frame.Planes)
            {
                SafeAdd(planeDictionary,plane);
            }
        }

        var congregatedFrame = new PlaneFrame();

        congregatedFrame.Now = offsetTimestamp;
        congregatedFrame.Planes = planeDictionary.Where(_ => _.Value.lat != null && _.Value.lon != null).Select(_ => _.Value).ToArray();
        congregatedFrame.Source = "congregation";
        congregatedFrame.Antenna = "congregator";

        await _infrastructure.UploadCongregatedPlanesAsync(congregatedFrame);

        var metadata = new PlaneFrameMetadata();
        metadata.Total = planeDictionary.Count();
        metadata.Detailed = congregatedFrame.Planes.Count();
        

        metadata.Antenna = congregatedFrame.Antenna;
        metadata.Hostname = congregatedFrame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(congregatedFrame.Now);

        await _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
    }


    private void SafeAdd(Dictionary<string,Plane> planeDictionary, Plane plane)
    {
        if(!planeDictionary.ContainsKey(plane.hexValue))
        {
            planeDictionary.Add(plane.hexValue,plane);
        }
        else
        {
            var currentRecord = planeDictionary.GetValueOrDefault(plane.hexValue);

            currentRecord.altitude = OverwriteIfNull(currentRecord.altitude,plane.altitude);
            currentRecord.category = OverwriteIfNull(currentRecord.category,plane.category);
            currentRecord.flight = OverwriteIfNull(currentRecord.flight,plane.flight);
            currentRecord.lat = OverwriteIfNull(currentRecord.lat,plane.lat);
            currentRecord.lon = OverwriteIfNull(currentRecord.lon,plane.lon);
            currentRecord.messages = OverwriteIfNull(currentRecord.messages,plane.messages);
            currentRecord.nucp = OverwriteIfNull(currentRecord.nucp,plane.nucp);
            currentRecord.rssi = OverwriteIfNull(currentRecord.rssi,plane.rssi);
            currentRecord.speed = OverwriteIfNull(currentRecord.speed,plane.speed);
            currentRecord.squawk = OverwriteIfNull(currentRecord.squawk,plane.squawk);
            currentRecord.track = OverwriteIfNull(currentRecord.track,plane.track);
            currentRecord.vert_rate = OverwriteIfNull(currentRecord.vert_rate,plane.vert_rate);

            planeDictionary[plane.hexValue] = currentRecord;
        }
    }

    private T OverwriteIfNull<T>(T current, T proposed) => (current == null ? proposed : current);
}
