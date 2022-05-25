using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using System.Collections.Generic;
using System.Linq;
using Inter.Domain;
using System;
using System.Diagnostics;

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
        var timer = new Stopwatch();
        try
        {
            
        timer.Start();
        var offsetTimestamp = timestamp - 1; // look at the previous previous second
        // compile from redis
        var planeDictionary = (await _infrastructure.CollectPlaneStatesAsync())
            .ToDictionary(_ => _.hexValue);

        //get deltas
        var deltas = await _infrastructure.CollectDeltaFramesAsync(offsetTimestamp);
        //sort by interval
        var combinedDeltas = deltas
            .OrderByDescending(_ => _.Interval)
        //compress into dictionary
            .Aggregate(new Dictionary<string,Plane>(), 
                (sum, frame) => CompilePlaneDeltas(sum, frame));

        //replace planes
        var deltasApplied = CompilePlaneDeltas(planeDictionary,new PlaneFrame() {Planes = combinedDeltas.Select(_ => _.Value).ToArray()});

        var updateStateTask = _infrastructure.UploadPlaneStates(planeDictionary.Where(_ => combinedDeltas.ContainsKey(_.Key)).Select(_ => _.Value));
        //Compile into plane frame
        var congregatedFrame = new PlaneFrame();

        congregatedFrame.Now = offsetTimestamp;
        congregatedFrame.Planes = deltasApplied
            .Where(_ => _.Value.lat != null && _.Value.lon != null)
            .Select(_ => _.Value)
            .ToArray();
        congregatedFrame.Source = "congregation";
        congregatedFrame.Antenna = "congregator";

        var uploadCongregationTask = _infrastructure.UploadCongregatedPlanesAsync(congregatedFrame);

        var metadata = new PlaneFrameMetadata();
        metadata.Total = deltasApplied.Count();
        metadata.Detailed = congregatedFrame.Planes.Count();
        

        metadata.Antenna = congregatedFrame.Antenna;
        metadata.Hostname = congregatedFrame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(congregatedFrame.Now);

        var uploadMetadataTask = _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
        await Task.WhenAll(updateStateTask,uploadCongregationTask,uploadMetadataTask);
        timer.Stop();
        Console.WriteLine($"This process took {timer.ElapsedMilliseconds} milliseconds");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex) ;
        }
    }
    private Dictionary<string, Plane> CompilePlaneDeltas(Dictionary<string, Plane> running, PlaneFrame applying)
    {
        foreach(var plane in applying.Planes)
        {
            SafeAdd(running,plane);
        }

        return running;

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

            currentRecord.altitude = OverwriteIfNotNull(currentRecord.altitude,plane.altitude);
            currentRecord.category = OverwriteIfNotNull(currentRecord.category,plane.category);
            currentRecord.flight = OverwriteIfNotNull(currentRecord.flight,plane.flight);
            currentRecord.lat = OverwriteIfNotNull(currentRecord.lat,plane.lat);
            currentRecord.lon = OverwriteIfNotNull(currentRecord.lon,plane.lon);
            currentRecord.messages = OverwriteIfNotNull(currentRecord.messages,plane.messages);
            currentRecord.nucp = OverwriteIfNotNull(currentRecord.nucp,plane.nucp);
            currentRecord.rssi = OverwriteIfNotNull(currentRecord.rssi,plane.rssi);
            currentRecord.speed = OverwriteIfNotNull(currentRecord.speed,plane.speed);
            currentRecord.squawk = OverwriteIfNotNull(currentRecord.squawk,plane.squawk);
            currentRecord.track = OverwriteIfNotNull(currentRecord.track,plane.track);
            currentRecord.vert_rate = OverwriteIfNotNull(currentRecord.vert_rate,plane.vert_rate);

            planeDictionary[plane.hexValue] = currentRecord;
        }
    }
    private T OverwriteIfNotNull<T>(T current, T proposed) => (proposed != null ? proposed : current); 
}
