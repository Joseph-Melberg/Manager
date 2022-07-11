using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class HeartbeatListenerServiceTests 
{
    private Mock<IHeartbeatListenerInfrastructureService> _infra;
    private HeartbeatListenerService _service;

    private string _deadNodeName = "parrot";

    private string _livingNodeName = "parrot kepper";

    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<IHeartbeatListenerInfrastructureService>();

        _infra.Setup( _ => _.GetHeartbeatStateAsync(It.Is<string>(_ => _ == _livingNodeName))).Returns(Task.FromResult(true));
        _infra.Setup( _ => _.GetHeartbeatStateAsync(It.Is<string>(_ => _ == _deadNodeName))).Returns(Task.FromResult(false));

        _service = new HeartbeatListenerService(_infra.Object);
    }
    [TestMethod]
    public async Task HeartbeatListenerService_Process_HeartbeatFromDeadNodeShouldAnnounce()
    {
        var standardMessage = new HeartbeatPayload()
        {
            Mac = "Mac adress",
            Name = _deadNodeName,
        };

        await _service.Process(standardMessage);

        var expected = new Heartbeat
        {
            announced = true,
            mac = standardMessage.Mac,
            name = standardMessage.Name,
            online = true,
        };

        _infra.Verify(_ => _.UpdateAsync(It.Is<Heartbeat>(_ => CompareHeartbeat(expected,_))));

    }
    [TestMethod]
    public async Task HeartbeatListenerService_Process_HeartbeatFromLivingNodeShouldNotAnnounce()
    {
        var standardMessage = new HeartbeatPayload()
        {
            Mac = "Mac adress",
            Name = _livingNodeName,
        };

        await _service.Process(standardMessage);

        var expected = new Heartbeat
        {
            announced = false,
            mac = standardMessage.Mac,
            name = standardMessage.Name,
            online = true,
        };

        _infra.Verify(_ => _.UpdateAsync(It.Is<Heartbeat>(_ => CompareHeartbeat(expected,_))));

    }

    private static bool CompareHeartbeat(Heartbeat expected, Heartbeat actual)
    {
        Assert.AreEqual(expected.mac,actual.mac,"Mac");
        Assert.AreEqual(expected.name,actual.name,"Name");
        Assert.AreEqual(expected.announced,actual.announced,"Announced");
        Assert.AreEqual(expected.online,actual.online,"Online");

        return true;
    }
}