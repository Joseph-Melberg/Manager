using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class PlaneCongregatorServiceTests
{
    private Mock<IPlaneCongregatorInfrastructureService> _infra;
    private PlaneCongregatorService _service;

    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<IPlaneCongregatorInfrastructureService>();



        _service = new PlaneCongregatorService(_infra.Object);
    }

    [TestMethod]
    public async Task PlaneCongregator_Process_Standard()
    {
        var planeFrame1 = new PlaneFrame();
        planeFrame1.Planes = new[]
        {
            new TimeAnotatedPlane()
            {
                Altitude = 1,
                AltitudeUpdated = 10,
                HexValue = "A",
                Latitude = 1,
                Longitude = 1,
                Nucp = "a",
                PositionUpdated = 10,
            },

            new TimeAnotatedPlane()
            {
                Speed = 1,
                SpeedUpdated = 13,
                HexValue = "B",
                Latitude = 10,
                Longitude = 11,
                Nucp = "b",
                PositionUpdated = 13,
            }
        };

        var planeFrame2 = new PlaneFrame();
        planeFrame2.Planes = new[]
        {
            new TimeAnotatedPlane()
            {
                Altitude = 2,
                AltitudeUpdated = 11,
                HexValue = "A",
                Latitude = 13,
                Longitude = 10,
                Nucp = "b",
                PositionUpdated = 3,
            },
            new TimeAnotatedPlane()
            {
                Category = "Cat",
                CategoryUpdated = 10,
                HexValue = "C",
                Latitude = 23,
                Longitude = 1,
                Nucp = "4",
                PositionUpdated = 5,
            }
        };
        var planeFrame3 = new PlaneFrame();
        planeFrame3.Planes = new[]
        {
            new TimeAnotatedPlane()
            {
                Speed = 4,
                SpeedUpdated = 12,
                HexValue = "B",
                Latitude = 123,
                Longitude = 0,
                Nucp = "4",
                PositionUpdated = 3,
            },
            new TimeAnotatedPlane()
            {
                Speed = 4,
                SpeedUpdated = 12,
                HexValue = "D",
            }
        };
        var planeFrames = GetPlanes(new [] {planeFrame1, planeFrame2, planeFrame3});
        
        _infra.Setup( _ => _.CollectPlaneStatesAsync(It.IsAny<long>())).Returns(planeFrames);

        await _service.CongregatePlaneInfoAsync(20);

        _infra.Verify( _ => _.UploadCongregatedPlanesAsync(It.Is<PlaneFrame>(_ => 
        _.Planes.Length == 3)));


    }

    private async IAsyncEnumerable<PlaneFrame> GetPlanes(IEnumerable<PlaneFrame> frames)
    {
        foreach(var planeFrame in frames)
        {
            await Task.Delay(1);
            yield return planeFrame;
        }
    }



}