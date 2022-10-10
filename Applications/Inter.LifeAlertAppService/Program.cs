﻿using System;
using System.IO;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Melberg.Application;

namespace Inter.LifeAlertAppService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().StartAsync();
}