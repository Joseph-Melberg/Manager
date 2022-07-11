using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Inter.DomainServices.Tests;

[TestClass]
public class MetronomeServiceTests
{
    private Mock<IMetronomeInfrastructureService> _infra;
    private MetronomeService _service;
    [TestInitialize]
    public void Initialize()
    {
        _infra = new Mock<IMetronomeInfrastructureService>();
        _service = new MetronomeService(_infra.Object);
    }

    [TestMethod]
    public async Task Metronome_Standard_Success()
    {
        var tokenSource = new CancellationTokenSource();

        var serviceTask = _service.StartAsync(tokenSource.Token);

        await Task.Delay(5000);
        tokenSource.Cancel();

        await serviceTask;

        _infra.Verify(_ => _.SendTick(),Times.Exactly(6));
    }
}