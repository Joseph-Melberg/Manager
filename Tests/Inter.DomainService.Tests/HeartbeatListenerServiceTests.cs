using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainService.Tests
{
    [TestClass]
    public class HeartbeastListenerServiceTests 
    {
        private Mock<IHeartbeatListenerInfrastructureService> _infra;
        private HeartbeatListenerService _service;

        private string _deadNodeName = "parrot";

        private string _livingNodeName = "parrot kepper";

        [TestInitialize]
        public async Task Initialize()
        {
            _infra = new Mock<IHeartbeatListenerInfrastructureService>();

            _infra.Setup( _ => _.GetHeartbeatStateAsync(It.Is<string>(_ => _ ==_livingNodeName))).Returns(Task.FromResult(true));
            _infra.Setup( _ => _.GetHeartbeatStateAsync(It.Is<string>(_ => _ ==_deadNodeName))).Returns(Task.FromResult(false));

            _service = new HeartbeatListenerService(_infra.Object);
        }
        [TestMethod]
        public async Task HeartbeatFromDeadNode_ShouldAnnounce()
        {
            var standardMessage = new HeartbeatMessage()
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
        public async Task HeartbeatFromLivingNode_ShouldNotAnnounce()
        {
            var standardMessage = new HeartbeatMessage()
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
}
