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
        //upload newest frame
        var insertTask = _infrastructure.IngestPlaneFrameAsync(frame);

        var metadataTask = HandleMetadata(frame);

        var deltaTask = CalculateDelta(frame);
        
        await Task.WhenAll(insertTask,metadataTask, deltaTask);
    }

    private async Task HandleMetadata(PlaneFrame frame)
    {
        var metadata = new PlaneFrameMetadata();
        metadata.Total = frame.Planes.Count();
        metadata.Detailed = frame.Planes.Where(_ => _.lat.HasValue && _.lon.HasValue).Count();

        metadata.Antenna = frame.Antenna;
        metadata.Hostname = frame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(frame.Now);

        await _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
    }

    private async Task CalculateDelta(PlaneFrame frame)
    {
        var planeSource = new PlaneSourceDefintion { Antenna = frame.Antenna, Node = frame.Source};

        var currentState = await _infrastructure.GetPlaneSourceStateAsync(planeSource);
        var updateStateTask = _infrastructure.SetPlaneSourceStateAsync(planeSource,frame);

        var delta = new PlaneFrameDelta()
        {
            Antenna = frame.Antenna,
            Now = frame.Now,
            Source = frame.Source,
            Interval = frame.Now - currentState.Now,
            Planes = CreateDelta(frame, currentState).ToArray()
        };

        var uploadDeltaTask = _infrastructure.SetPlaneSourceDeltaAsync(planeSource,delta);

        await Task.WhenAll(updateStateTask, uploadDeltaTask);

    }
    private IEnumerable<Plane> CreateDelta(PlaneFrame entering, PlaneFrame current)
    {
        var record = current.Planes.ToDictionary(_ => _.hexValue);

        foreach(var newPlane in entering.Planes.Where(_ => !String.IsNullOrEmpty(_.hexValue)))
        {
            if(!record.ContainsKey(newPlane.hexValue))
            {
                yield return newPlane;
            }
            else
            {
                yield return CalculateDifference(newPlane,record);
            }
        }
    }

    private Plane CalculateDifference(Plane selected, Dictionary<string, Plane> current)
    {
        if(!current.ContainsKey(selected.hexValue))
        {
            return selected;
        }

        var currentRecord = current[selected.hexValue];

        return new Plane() 
        {
            hexValue = selected.hexValue,
            altitude = CompareOrNull(currentRecord.altitude, selected.altitude),
            category = CompareOrNull(currentRecord.category, selected.category),
            flight = CompareOrNull(currentRecord.flight, selected.flight),
            lat = CompareOrNull(currentRecord.lat, selected.lat),
            lon = CompareOrNull(currentRecord.lon, selected.lon),
            messages = CompareOrNull(currentRecord.messages, selected.messages),
            nucp = CompareOrNull(currentRecord.nucp, selected.nucp),
            rssi = CompareOrNull(currentRecord.rssi, selected.rssi),
            speed = CompareOrNull(currentRecord.speed, selected.speed),
            squawk = CompareOrNull(currentRecord.squawk, selected.squawk),
            track = CompareOrNull(currentRecord.track, selected.track), 
            vert_rate = CompareOrNull(currentRecord.vert_rate, selected.vert_rate)
        };
    }

    private string CompareOrNull(string currentValue, string selectedValue) =>
        (!string.IsNullOrEmpty(selectedValue) &&
            (string.IsNullOrEmpty(currentValue) ? string.Compare(currentValue,selectedValue) != 0 : true))
            ? selectedValue : (string) null;
    private float? CompareOrNull(float? currentValue, float? selectedValue) =>
        (selectedValue.HasValue &&  
            (currentValue.HasValue ? currentValue.Value != selectedValue : true))
         ? selectedValue : (float?)null;    
    private int? CompareOrNull(int? currentValue, int? selectedValue) =>
        (selectedValue.HasValue &&  
            (currentValue.HasValue ? currentValue.Value != selectedValue : true))
         ? selectedValue : null;    

}