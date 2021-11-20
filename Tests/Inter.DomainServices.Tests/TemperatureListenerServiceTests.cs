using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests
{
    [TestClass]
    public class TemperatureListenerServiceTests
    {
        private Mock<ITemperatureListenerInfrastructureService> _infra;
        private TemperatureListenerService _service;
        private TemperatureMark _mark;
        [TestInitialize]
        public void TestInitialize()
        {
            _mark = new TemperatureMark
            {
                HostName = ":)"
            };
            _infra = new Mock<ITemperatureListenerInfrastructureService>();

            _service = new TemperatureListenerService(_infra.Object);
        }

        [TestMethod]
        public async Task TemperatureListenerService_RecordTempAsync_Standard()
        {
            var input = new TemperatureMark[] {_mark};

            await _service.RecordTempAsync(input);

            _infra.Verify(_ => _.InsertTemperatureAsync(_mark),Times.Once);
            _infra.Verify(_ => _.SaveRecordsAsync(), Times.Once());
        }
    }
}