using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class PlaneIngestorService : IPlaneIngestorService
{
    private readonly IPlaneIngestorInfrastructureService _infrastructure;
    public PlaneIngestorService(IPlaneIngestorInfrastructureService infrastructureService)
    {
        _infrastructure = infrastructureService;
    }

    public async Task HandleMessageAsync(PlaneFrame frame)
    {
        var insertTask = _infrastructure.IngestPlaneFrameAsync(frame);

        var metadataTask = HandleMetadata(frame);

        await Task.WhenAll(insertTask,metadataTask);
    }

    private async Task HandleMetadata(PlaneFrame frame)
    {
        var metadata = new PlaneFrameMetadata();
        metadata.Total = frame.Planes.Count();
        metadata.Detailed = frame.Planes.Where(_ => _.Latitude.HasValue && _.Longitude.HasValue).Count();

        metadata.Antenna = frame.Antenna;
        metadata.Hostname = frame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(frame.Now);

        await _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
    }


    private TimeAnotatedPlane CalculateDifference(TimeAnotatedPlane selected, Dictionary<string, TimeAnotatedPlane> current)
    {
        if(!current.ContainsKey(selected.HexValue))
        {
            return selected;
        }

        var currentRecord = current[selected.HexValue];

        var updatePosition = CompareUpdated(true, false, currentRecord.PositionUpdated, selected.PositionUpdated);

        return new TimeAnotatedPlane() 
        {
            HexValue = selected.HexValue,
            Altitude = CompareUpdated(currentRecord.Altitude, selected.Altitude, currentRecord.AltitudeUpdated, selected.AltitudeUpdated),
            Category = CompareUpdated(currentRecord.Category, selected.Category, currentRecord.CategoryUpdated, selected.CategoryUpdated),
            Flight = CompareUpdated(currentRecord.Flight, selected.Flight, currentRecord.FlightUpdated, selected.FlightUpdated),
            Latitude = (updatePosition ? currentRecord.Latitude: selected.Latitude),
            Longitude = (updatePosition ? currentRecord.Longitude: selected.Longitude),
            Nucp = (updatePosition ? currentRecord.Nucp: selected.Nucp),
            Rssi = 0,
            Speed = CompareUpdated(currentRecord.Speed, selected.Speed, currentRecord.SpeedUpdated, selected.SpeedUpdated),
            Squawk = CompareUpdated(currentRecord.Squawk, selected.Squawk, currentRecord.SquawkUpdated, selected.SquawkUpdated),
            Track = CompareUpdated(currentRecord.Track, selected.Track, currentRecord.TrackUpdated, selected.TrackUpdated), 
            Messages = "0",
            VerticleRate = CompareUpdated(currentRecord.VerticleRate, selected.VerticleRate, currentRecord.VerticleRateUpdated, selected.VerticleRateUpdated)
        };
    }

    private T CompareUpdated<T>(T currentValue, T selectedValue, ulong? currentUpdated, ulong? selectedUpdated) =>
        (currentUpdated ?? 0) > (selectedUpdated ?? 0) ? currentValue : selectedValue;
}