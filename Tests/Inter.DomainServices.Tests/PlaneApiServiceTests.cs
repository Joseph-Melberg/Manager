using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class PlaneApiServiceTests
{
    private Mock<IPlaneApiInfrastructureService> _infra;

    private PlaneApiDomainService _service;
    private PlaneFrame _frame;

    [TestInitialize]
    public void TestInitialize()
    {
        _frame = new PlaneFrame()
        {
            Now = 1,
            Planes = new TimeAnotatedPlane[]
            {

            }
        };

        _infra = new Mock<IPlaneApiInfrastructureService>();

        _infra.Setup(_ => _.GetFrameAsync(It.IsAny<long>())).Returns(Task.FromResult(_frame));

        _service = new PlaneApiDomainService(_infra.Object);
    }

    [TestMethod]
    public async Task PlaneApiService_GetFrameAsync_Standard()
    {
        var result = await _service.GetFrameAsync(1);

        Assert.AreEqual(_frame.Now,result.Now);
    }
}