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
        var offsetTimestamp = timestamp - 1; // look at the previous previous second
        
        Console.WriteLine($"Congregating {offsetTimestamp}");

        var totalState = new Dictionary<string,TimeAnotatedPlane>();

        await foreach(var planeFrame in _infrastructure.CollectPlaneStatesAsync(offsetTimestamp))
        {
            foreach(var plane in planeFrame.Planes)
            {
                SafeAdd(totalState,plane);
            }
        }

        var congregatedFrame = new PlaneFrame();

        congregatedFrame.Now = offsetTimestamp;
        congregatedFrame.Planes = totalState
            .Where(_ => 
                _.Value.Latitude != null && 
                _.Value.Longitude != null && 
                (((long)(_.Value.PositionUpdated.Value)/1000 + 30 )> offsetTimestamp))
            .Select(_ => _.Value)
            .ToArray();
        congregatedFrame.Source = "congregation";
        congregatedFrame.Antenna = "congregator";
        
        var uploadCongregationTask = _infrastructure.UploadCongregatedPlanesAsync(congregatedFrame);

        var metadata = new PlaneFrameMetadata();
        
        metadata.Total = congregatedFrame.Planes.Count();
        metadata.Detailed = congregatedFrame.Planes.Count();
        metadata.Antenna = congregatedFrame.Antenna;
        metadata.Hostname = congregatedFrame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(congregatedFrame.Now);

        var uploadMetadataTask = _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
        await Task.WhenAll(uploadCongregationTask,uploadMetadataTask);
    }

    private T CompareUpdated<T>(T currentValue, T selectedValue, ulong? currentUpdated, ulong? selectedUpdated) =>
        (currentUpdated ?? 0) > (selectedUpdated ?? 0) ? currentValue : selectedValue;
    private void SafeAdd(Dictionary<string,TimeAnotatedPlane> planeDictionary, TimeAnotatedPlane plane)
    {
        if(!planeDictionary.ContainsKey(plane.HexValue))
        {
            planeDictionary.Add(plane.HexValue,plane);
        }
        else
        {
            var currentRecord = planeDictionary.GetValueOrDefault(plane.HexValue);
            var updatePosition = CompareUpdated(true, false, currentRecord.PositionUpdated, plane.PositionUpdated);

            currentRecord.Altitude = CompareUpdated(currentRecord.Altitude, plane.Altitude, currentRecord.AltitudeUpdated, plane.AltitudeUpdated);
            currentRecord.AltitudeUpdated = BestUpdated(currentRecord.AltitudeUpdated,plane.AltitudeUpdated);
            currentRecord.Category = CompareUpdated(currentRecord.Category,plane.Category, currentRecord.CategoryUpdated, plane.CategoryUpdated);
            currentRecord.CategoryUpdated = BestUpdated(currentRecord.CategoryUpdated, plane.CategoryUpdated);
            currentRecord.Flight = CompareUpdated(currentRecord.Flight, plane.Flight, currentRecord.FlightUpdated, plane.FlightUpdated);
            currentRecord.FlightUpdated = BestUpdated(currentRecord.FlightUpdated, plane.FlightUpdated);
            currentRecord.Latitude = (updatePosition ? currentRecord.Latitude : plane.Latitude);
            currentRecord.Longitude = (updatePosition ? currentRecord.Longitude : plane.Longitude);
            currentRecord.Messages = "0";
            currentRecord.Nucp = (updatePosition ? currentRecord.Nucp : plane.Nucp);
            currentRecord.PositionUpdated = BestUpdated(currentRecord.PositionUpdated, plane.PositionUpdated);
            currentRecord.Rssi = 0; //not important
            currentRecord.Speed = CompareUpdated(currentRecord.Speed, plane.Speed,currentRecord.SpeedUpdated, plane.SpeedUpdated);
            currentRecord.SpeedUpdated = BestUpdated(currentRecord.SpeedUpdated,plane.SpeedUpdated);
            currentRecord.Squawk = CompareUpdated(currentRecord.Squawk, plane.Squawk, currentRecord.SquawkUpdated, plane.SquawkUpdated);
            currentRecord.SquawkUpdated = BestUpdated(currentRecord.SpeedUpdated, plane.SquawkUpdated);
            currentRecord.Track = CompareUpdated(currentRecord.Track, plane.Track, currentRecord.TrackUpdated, plane.TrackUpdated);
            currentRecord.TrackUpdated = BestUpdated(currentRecord.TrackUpdated, plane.TrackUpdated);
            currentRecord.VerticleRate = CompareUpdated(currentRecord.VerticleRate, plane.VerticleRate, currentRecord.VerticleRateUpdated, plane.VerticleRateUpdated);
            currentRecord.VerticleRateUpdated = BestUpdated(currentRecord.VerticleRateUpdated, plane.VerticleRateUpdated);

            planeDictionary[plane.HexValue] = currentRecord;
        }
    }

    private ulong BestUpdated(ulong? currentUpdated, ulong? selectedUpdated) => 
        (currentUpdated ?? 0) > (selectedUpdated ?? 0) ? 
        currentUpdated ?? 0 : selectedUpdated ?? 0;
}
