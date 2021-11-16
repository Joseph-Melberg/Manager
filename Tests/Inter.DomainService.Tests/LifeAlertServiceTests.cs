using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainService.Tests
{
    [TestClass]
    public class LifeAlertServiceTests 
    {
        private Mock<ILifeAlertInfrastructureService> _infra;
        private LifeAlertService _service;

        private string _mac = "mac address";
        private string _nodeName = "Bob";

        private Heartbeat _deadNodeUnannounced;
        private Heartbeat _deadNodeAnnounced;
        private Heartbeat _livingNodeUnannounced;
        private Heartbeat _livingNodeAnnounced;

        [TestInitialize]
        public async Task Initialize()
        {
            _deadNodeAnnounced= new Heartbeat
            {
                announced = true,
                mac = _mac,
                name = _nodeName,
                online = false,
            };

            _deadNodeUnannounced = new Heartbeat
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = false,
            };

            _livingNodeAnnounced= new Heartbeat
            {
                announced = true,
                mac = _mac,
                name = _nodeName,
                online = true,
            };

            _livingNodeUnannounced= new Heartbeat
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = true,
            };


            _infra = new Mock<ILifeAlertInfrastructureService>();

            _service = new LifeAlertService(_infra.Object);
        }
        [TestMethod]
        public async Task ProcessDeadUnannounced()
        {
            _infra.Setup( _ => _.GetStatusesAsync()).Returns(Task.FromResult(new List<Heartbeat>(){_deadNodeUnannounced}));

            await _service.Do();

            var expected = new Heartbeat()
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = false
            };

            _infra.Verify(_ => _.UpdateNode(It.Is<Heartbeat>(r => CompareHeartbeat(expected,r))),Times.Once);
        }
        [TestMethod]
        public async Task ProcessDeadAnnounced()
        {
            _infra.Setup( _ => _.GetStatusesAsync()).Returns(Task.FromResult(new List<Heartbeat>(){_deadNodeAnnounced}));

            await _service.Do();

            var expected = new Heartbeat()
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = false
            };

            _infra.Verify(_ => _.UpdateNode(It.IsAny<Heartbeat>()),Times.Never);
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
