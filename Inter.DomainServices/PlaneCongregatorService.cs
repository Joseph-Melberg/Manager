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

            var nodeStates = await _infrastructure.CollectPlaneStatesAsync();

        }
        catch (System.Exception)
        {
            
            throw;
        }        






        try
        {
            
        timer.Start();
        var offsetTimestamp = timestamp - 1; // look at the previous previous second
        // compile from redis
        var planeDictionary = (await _infrastructure.CollectPlaneStatesAsync())
            .ToDictionary(_ => _.HexValue);

        //get deltas
        var deltas = await _infrastructure.CollectDeltaFramesAsync(offsetTimestamp);
        var combinedDeltas = deltas
        //compress into dictionary
            .Aggregate(new Dictionary<string,TimeAnotatedPlane>(), 
                (sum, frame) => CompilePlaneDeltas(sum, frame));

        //replace planes
        var deltasApplied = CompilePlaneDeltas(planeDictionary,new PlaneFrame() {Planes = combinedDeltas.Select(_ => _.Value).ToArray()});

        var updateStateTask = _infrastructure.UploadPlaneStates(planeDictionary.Where(_ => combinedDeltas.ContainsKey(_.Key)).Select(_ => _.Value));
        //Compile into plane frame
        var congregatedFrame = new PlaneFrame();

        congregatedFrame.Now = offsetTimestamp;
        congregatedFrame.Planes = deltasApplied
            .Where(_ => _.Value.Latitude != null && _.Value.Longitude != null)
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
    private Dictionary<string, TimeAnotatedPlane> CompilePlaneDeltas(Dictionary<string, TimeAnotatedPlane> running, PlaneFrame applying)
    {
        foreach(var plane in applying.Planes)
        {
            SafeAdd(running,plane);
        }

        return running;

    }
    private void SafeAdd(Dictionary<string,TimeAnotatedPlane> planeDictionary, TimeAnotatedPlane plane)
    {
        if(!planeDictionary.ContainsKey(plane.HexValue))
        {
            planeDictionary.Add(plane.HexValue,plane);
        }
        else
        {
            var currentRecord = planeDictionary.GetValueOrDefault(plane.HexValue);

            currentRecord.Altitude = OverwriteIfNotNull(currentRecord.Altitude,plane.Altitude);
            currentRecord.Category = OverwriteIfNotNull(currentRecord.Category,plane.Category);
            currentRecord.Flight = OverwriteIfNotNull(currentRecord.Flight,plane.Flight);
            currentRecord.Latitude = OverwriteIfNotNull(currentRecord.Latitude,plane.Latitude);
            currentRecord.Longitude = OverwriteIfNotNull(currentRecord.Longitude,plane.Longitude);
            currentRecord.Messages = "0";
            currentRecord.Nucp = OverwriteIfNotNull(currentRecord.Nucp,plane.Nucp);
            currentRecord.Rssi = 0; 
            currentRecord.Speed = OverwriteIfNotNull(currentRecord.Speed,plane.Speed);
            currentRecord.Squawk = OverwriteIfNotNull(currentRecord.Squawk,plane.Squawk);
            currentRecord.Track = OverwriteIfNotNull(currentRecord.Track,plane.Track);
            currentRecord.VerticleRate = OverwriteIfNotNull(currentRecord.VerticleRate,plane.VerticleRate);

            planeDictionary[plane.HexValue] = currentRecord;
        }
    }

    private T OverwriteIfNotNull<T>(T current, T proposed) => (proposed != null ? proposed : current); 
}
