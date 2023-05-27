using System.Diagnostics;
using Inter.DomainServices.Core;
using Inter.PlaneIngestorService.Mappers;
using Inter.PlaneIngestorService.Messages;
using Inter.PlaneIngestorService.Models;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Newtonsoft.Json;

namespace Inter.PlaneIngestorService.Application;

public class Processor : IStandardConsumer
{
    private readonly IPlaneIngestorDomainService _service;
    private readonly IJsonToObjectTranslator<PlaneIngestionMessage> _translator;
    public Processor(
        IPlaneIngestorDomainService service,
        IJsonToObjectTranslator<PlaneIngestionMessage> translator
    ) 
    {
        _service = service;
        _translator = translator;
    } 
    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        try
        {
            var timer = new Stopwatch();
            timer.Start();
            var package = _translator.Translate(message);
            if(package == null)
            {
                throw new Exception($"Could not process {message}");
            }
            var domain = package.ToDomain();
            await _service.HandleMessageAsync(domain);

            var elapsed = timer.ElapsedMilliseconds;
            Console.WriteLine($"Processing {domain.Source}_{domain.Antenna} took {elapsed}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}