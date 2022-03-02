using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.PlaneListenerService.Models;
using Inter.PlaneListenerService.Mappers;
using Newtonsoft.Json;
using Melberg.Infrastructure.Rabbit.Consumers;
using System;
using System.Diagnostics;
using Melberg.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Translator;
using Inter.PlaneListenerService.Messages;

namespace Inter.PlaneListenerService.Application;
public class PlaneProcessor : IStandardConsumer
{

    private readonly IPlaneListenerService _service;
    private readonly IJsonToObjectTranslator<PlaneMessage> _translator;
    public PlaneProcessor(
        IPlaneListenerService service,
        IJsonToObjectTranslator<PlaneMessage> translator)
        {
            _service = service;
            _translator = translator;
        } 

    public async Task ConsumeMessageAsync(Message message) 
    {

        try
        {
            var package = _translator.Translate(message);
            await _service.HandleMessageAsync(package.ToDomain());

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}