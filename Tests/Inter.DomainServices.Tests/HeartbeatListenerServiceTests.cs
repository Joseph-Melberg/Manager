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

    private static string _nodeName = "NODE";
    private static Heartbeat _noRecord = null;
    private static Heartbeat _existingRecord = new Heartbeat()
    {
        announced = true,
        mac = "mac",
        name = "NODE",
        online = true,
        timestamp = System.DateTime.Now
    };

    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<IHeartbeatListenerInfrastructureService>();
        _service = new HeartbeatListenerService(_infra.Object);
    }

    [TestMethod]

    public async Task HeartbeatListenerService_Process_NoRecord()
    {
        _infra.Setup( _ => _.GetHeartbeatStateAsync(It.IsAny<string>())).Returns(Task.FromResult(_noRecord));
        var standardMessage = new HeartbeatPayload()
        {
            Mac = "Mac adress",
            Name = _nodeName,
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
    [TestMethod]
    public async Task HeartbeatListenerService_Process_Existing()
    {
        _infra.Setup( _ => _.GetHeartbeatStateAsync(It.IsAny<string>())).Returns(Task.FromResult(_existingRecord));
        var standardMessage = new HeartbeatPayload()
        {
            Mac = "Mac adress",
            Name = _nodeName,
        };

        await _service.Process(standardMessage);

        var expected = new Heartbeat
        {
            announced = _existingRecord.announced,
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