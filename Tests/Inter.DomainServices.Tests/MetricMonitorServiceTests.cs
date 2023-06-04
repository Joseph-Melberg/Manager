using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Inter.Infrastructure.Core;
using System.Threading.Tasks;
using Inter.Domain;
using System;

namespace Inter.DomainServices.Tests;

[TestClass]
public class MetricMonitorServiceTests
{
    private Mock<IMetricMonitorInfrastructureService> _infraMock;

    private MetricMonitorDomainService _service;

    [TestInitialize]
    public void Initialize()
    {
        _infraMock = new Mock<IMetricMonitorInfrastructureService>();

        _service = new MetricMonitorDomainService(_infraMock.Object);
    }


    [TestMethod]
    public async Task MetricMonitor_RecordMetric_Success()
    {
        var name = "test";
        var date = DateTime.UtcNow;
        long duration = 10;
        var happyMetric = new Metric(){Application = name, TimeInMS = duration, TimeStamp = date};
        await _service.RecordMetricAsync(happyMetric);

        _infraMock.Verify(_ => _.RecordMetricAsync(
            It.Is<Metric>(o => 
                o.TimeInMS == duration &&
                o.TimeStamp == date &&
                o.Application == name)),
                Times.Once);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    public async Task MetricMonitor_RecordMetric_ApplicationEmpty(string name)
    {
        var date = DateTime.UtcNow;
        long duration = 10;
        var happyMetric = new Metric(){Application = name, TimeInMS = duration, TimeStamp = date};
        await _service.RecordMetricAsync(happyMetric);

        _infraMock.Verify(_ => _.RecordMetricAsync(It.IsAny<Metric>()), Times.Never);

    }
    
}