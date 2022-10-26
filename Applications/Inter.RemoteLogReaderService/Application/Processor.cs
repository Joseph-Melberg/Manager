using Inter.RemoteLogReaderService.Messsage;
using Melberg.Infrastructure.Rabbit.Consumers;
using Melberg.Infrastructure.Rabbit.Messages;
using System.IO;
using Melberg.Infrastructure.Rabbit.Translator;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Inter.RemoteLogReaderService;

public class Processor : IStandardConsumer
{
    private readonly IJsonToObjectTranslator<LogMessage> _translator;
    public Processor(
        IJsonToObjectTranslator<LogMessage> translator
    ) 
    {
        //_service = service;
        _translator = translator;
    } 
    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        try
        {
            var dict = message.Headers.ToDictionary(
                _ => _.Key, 
                _ => Encoding.UTF8.GetString(_.Value as byte[] ?? new byte[0]));

            var text = Encoding.UTF8.GetString(message.Body);
            var hasProg = dict.TryGetValue("PROGRAM", out var prog);
            Console.WriteLine(dict["HOST"] + " " + dict["DATE"]+" " + dict["PRIORITY"] + " " + (hasProg ? dict["PROGRAM"] + " " : "") + dict["MESSAGE"]);
            //await _service.HandleMessageAsync(package.ToDomain());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}