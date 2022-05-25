using System.Diagnostics;
using Inter.DomainServices.Core;
using Inter.PlaneIngestorService.Mappers;
using Inter.PlaneIngestorService.Messages;
using Inter.PlaneIngestorService.Models;
using Melberg.Infrastructure.Rabbit.Consumers;
using Melberg.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Translator;
using Newtonsoft.Json;

namespace Inter.PlaneIngestorService.Application;

public class Processor : IStandardConsumer
{
    private readonly IPlaneIngestorService _service;
    private readonly IJsonToObjectTranslator<PlaneIngestionMessage> _translator;
    public Processor(
        IPlaneIngestorService service,
        IJsonToObjectTranslator<PlaneIngestionMessage> translator
    ) 
    {
        _service = service;
        _translator = translator;
    } 
    public async Task ConsumeMessageAsync(Message message)
    {
        try
        {
            var package = _translator.Translate(message);
            if(package == null)
            {
                throw new Exception($"Could not process {message}");
            }
            Console.WriteLine("I am running");
            await _service.HandleMessageAsync(package.ToDomain());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}