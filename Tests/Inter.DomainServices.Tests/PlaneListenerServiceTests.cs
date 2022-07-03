using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;
[TestClass]
public class PlaneListenerServiceTests
{
    private Mock<IPlaneListenerInfrastructureService> _infra;

    private PlaneListenerService _service;

    private PlaneFrame _frame;

    private TimeAnotatedPlane _plane;

    [TestInitialize]
    public void TestInitialize()
    {
        _plane = new TimeAnotatedPlane()
        {
            Altitude = 1
        };
        _frame = new PlaneFrame()
        {
            Now = 1,
            Planes = new[]
            {
                _plane
            }
        };

        _infra = new Mock<IPlaneListenerInfrastructureService>();

        _service = new PlaneListenerService(_infra.Object);
    }

    [TestMethod]
    public async Task PlaneListenerService_Process_Standard()
    {
        await _service.HandleMessageAsync(_frame);

        _infra.Verify(_ => _.AddPlaneFrameAsync(_frame),Times.Once);
    }
}