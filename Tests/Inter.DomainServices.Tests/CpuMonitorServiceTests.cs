using System;
using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class CpuMonitorTests
{
    private Mock<ICpuMonitorInfrastructureService> _infra;
    private CpuMonitorDomainService _service;

    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<ICpuMonitorInfrastructureService>();
        _service = new CpuMonitorDomainService(_infra.Object);
    }

    [TestMethod]
    public async Task CpuMonitorDomainService_Process_Standard()
    {
        var host = "host";
        var time = DateTime.Now;
        var value = 1.2f;
        var input = new CpuUtilization() {Host = host, TimeStamp = time, Utilization = value};
        await _service.RecordAsync(input, CancellationToken.None);

        var result =_infra.Invocations[0].Arguments[0] as CpuUtilization;

        Assert.AreEqual(host, result.Host);
        Assert.AreEqual(time, result.TimeStamp);
        Assert.AreEqual(value, result.Utilization);
    }
}