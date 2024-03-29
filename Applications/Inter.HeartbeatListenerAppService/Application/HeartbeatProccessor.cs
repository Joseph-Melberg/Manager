﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.HeartbeatListenerAppService.Mappers;
using Inter.HeartbeatListenerAppService.Messages;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.HeartbeatListenerAppService.Application;

public class HeartbeatProcessor : IStandardConsumer
{

    private readonly IHeartbeatListenerDomainService _service;
    private readonly IJsonToObjectTranslator<HeartbeatMessage> _translator;
    public HeartbeatProcessor(
        IHeartbeatListenerDomainService service,
        IJsonToObjectTranslator<HeartbeatMessage> translator
        )
    {
        _translator = translator;
        _service = service;
    }
    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        Console.WriteLine(" [x] Received {0} at {1}", message, DateTime.Now);
        Stopwatch watch = new Stopwatch();
        watch.Start();
        try
        {
            var payload = _translator.Translate(message);
            await _service.Process(payload.ToDomain());
        }
        catch (Exception ex)
        {
            Console.WriteLine("That didn't work" + ex);
        }
        watch.Stop();
        Console.WriteLine($"Process took {watch.ElapsedMilliseconds}");
    }
}
