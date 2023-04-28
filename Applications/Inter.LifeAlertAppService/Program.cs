using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using MelbergFramework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.LifeAlertAppService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}