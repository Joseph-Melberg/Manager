using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests
{
    [TestClass]
    public class LifeAlertServiceTests 
    {
        private Mock<ILifeAlertInfrastructureService> _infra;
        private Mock<IEmailRecipientConfiguration> _emailConfig;
        private Mock<ILifeAlertRateConfiguration> _rateConfig;
        private LifeAlertService _service;
        private string _mac = "mac address";
        private string _nodeName = "Bob";
        private Heartbeat _livingNodeDied;
        private Heartbeat _deadNodeBecameAlive;
        private Heartbeat _deadNodeAnnounced;
        private Heartbeat _livingNodeAnnounced;
        private string _email => "howardtheduck@marvel.com";
        private int _rate => 5;


        [TestInitialize]
        public void Initialize()
        {
            _deadNodeBecameAlive= new Heartbeat
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = true,
                timestamp = DateTime.Now
            };

            _livingNodeDied = new Heartbeat
            {
                announced = true,
                mac = _mac,
                name = _nodeName,
                online = true,
                timestamp = DateTime.Now.Subtract(new TimeSpan(0,_rate,59))
            };

            _livingNodeAnnounced= new Heartbeat
            {
                timestamp = DateTime.Now,
                announced = true,
                mac = _mac,
                name = _nodeName,
                online = true,
            };

            _deadNodeAnnounced= new Heartbeat
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = false,
            };

            _infra = new Mock<ILifeAlertInfrastructureService>();
            _rateConfig = new Mock<ILifeAlertRateConfiguration>();
            _emailConfig = new Mock<IEmailRecipientConfiguration>();

            _rateConfig.Setup(_ => _.Rate).Returns(_rate);
            _emailConfig.Setup(_ => _.Recipient).Returns(_email);

            _service = new LifeAlertService(_infra.Object,_rateConfig.Object,_emailConfig.Object);
        }
        [TestMethod]
        public async Task LifeAlertService_ProcessNodeUp()
        {
            _infra.Setup( _ => _.GetStatusesAsync()).Returns(Task.FromResult(new List<Heartbeat>(){_deadNodeBecameAlive}));

            await _service.Do();

            var expected = new Heartbeat()
            {
                announced = true,
                mac = _mac,
                name = _nodeName,
                online = true
            };

            _infra.Verify(_ => _.UpdateNodeAsync(It.Is<Heartbeat>(r => CompareHeartbeat(expected,r))),Times.Once);
            _infra.Verify(_ => _.SendMessage(It.Is<string>(s => String.Compare(s, _email)==0),It.IsAny<string>(),It.IsAny<string>()),Times.Once);
        }

        [TestMethod]
        public async Task LifeAlertService_ProcessNodeDown()
        {
            _infra.Setup( _ => _.GetStatusesAsync()).Returns(Task.FromResult(new List<Heartbeat>(){_livingNodeDied}));

            await _service.Do();

            var expected = new Heartbeat()
            {
                announced = false,
                mac = _mac,
                name = _nodeName,
                online = false
            };

            _infra.Verify(_ => _.UpdateNodeAsync(It.Is<Heartbeat>(r => CompareHeartbeat(expected,r))),Times.Once);
        }

        [TestMethod]
        public async Task LifeAlertService_NothingHappened()
        {
            _infra.Setup( _ => _.GetStatusesAsync()).Returns(Task.FromResult(new List<Heartbeat>(){_livingNodeAnnounced,_deadNodeAnnounced}));

            await _service.Do();

            _infra.Verify(_ => _.UpdateNodeAsync(It.IsAny<Heartbeat>()),Times.Never);
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
