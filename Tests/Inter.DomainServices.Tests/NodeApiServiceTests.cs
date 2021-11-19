using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests
{
    [TestClass]
    public class NodeApiServiceTests
    {
        private Mock<INodeApiInfrastructureService> _infra;
        private NodeApiService _service;
        private DateTime _time = DateTime.UtcNow;
        private List<Heartbeat> _current;
        [TestInitialize]
        public void Initialize()
        {
            _current = new List<Heartbeat>
            {
                new Heartbeat
                {
                    announced = false,
                    mac = ":)",
                    name = "Namey",
                    online = false,
                    timestamp = _time
                },
                new Heartbeat
                {
                    announced = false,
                    mac = ":(",
                    name = "John",
                    online = true,
                    timestamp = _time
                }
            };
            _infra = new Mock<INodeApiInfrastructureService>();

            _infra.Setup(_ => _.GetStatiAsync()).Returns(Task.FromResult(_current));

            _service = new NodeApiService(_infra.Object);
        }

        [TestMethod]
        public async Task NodeApiService_GetUpCount_OnlyOnePath()
        {
            var result = await _service.GetUpCountAsync();

            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public async Task NodeApiService_GetListAsync_OnlyOnePath()
        {
            var result = await _service.GetListAsync();

            Assert.AreEqual(result.Count,_current.Count);
        }
    }
}