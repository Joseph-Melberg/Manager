using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class PlaneIngestorServiceTests
{
    private Mock<IPlaneIngestorInfrastructureService> _infraMock;
    private PlaneIngestorService _service;
    private PlaneFrame _frame;
    private TimeAnotatedPlane _plane;

    [TestInitialize]
    public void Initialize()
    {
        _plane = new TimeAnotatedPlane()
        {
            Altitude = 1,
            HexValue = "a",
        };
        _frame = new PlaneFrame()
        {
            Now = 1,
            Planes = new[]
            {
                _plane
            }
        };

        _infraMock = new Mock<IPlaneIngestorInfrastructureService>();
        _service = new PlaneIngestorService(_infraMock.Object);
    }

    [TestMethod]
    public async Task PlaneIngestorService_StandardInput_Pass()
    {
        await _service.HandleMessageAsync(_frame);

        _infraMock.Verify(_ => _.IngestPlaneFrameAsync(It.IsAny<PlaneFrame>()),Times.Once());
    } 
}