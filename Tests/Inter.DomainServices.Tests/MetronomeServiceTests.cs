using System;
using System.Threading;
using System.Threading.Tasks;
using Inter.Common.Clock;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class MetronomeServiceTests
{
    private Mock<IMetronomeInfrastructureService> _infra;
    private Mock<IClock> _clockMock;
    private DateTime _time;
    private MetronomeDomainService _service;
    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<IMetronomeInfrastructureService>();
        _clockMock = new Mock<IClock>();
        _service = new MetronomeDomainService(_infra.Object,_clockMock.Object);
    }

    [TestMethod]
    public async Task Metronome_Standard_Success()
    {
        var tokenSource = new CancellationTokenSource();
        var time = new DateTime(1,1,1,1,1,0);
        _clockMock.Setup(_ => _.GetUtcNow()).Returns( new DateTime(1,1,1,1,1,0));

        var serviceTask = _service.StartAsync(tokenSource.Token);

        await Task.Delay(400);
        tokenSource.Cancel();

        await serviceTask;

        _infra.Verify(_ => _.SendTick(),Times.Exactly(1));
        _infra.Verify(_ => _.SendMinuteTick(),Times.Exactly(1));
    }

    [TestMethod]
    public async Task Metronome_Standard_Just_Second()
    {
        var tokenSource = new CancellationTokenSource();
        var time = new DateTime(1,1,1,1,1,0);
        _clockMock.Setup(_ => _.GetUtcNow()).Returns( new DateTime(1,1,1,1,1,1));

        var serviceTask = _service.StartAsync(tokenSource.Token);

        await Task.Delay(400);
        tokenSource.Cancel();

        await serviceTask;

        _infra.Verify(_ => _.SendTick(),Times.Exactly(1));
        _infra.Verify(_ => _.SendMinuteTick(),Times.Exactly(0));
    }
}