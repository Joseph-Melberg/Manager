using System;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class LifeAlertService : ILifeAlertService
    {
        private readonly ILifeAlertInfrastructureService _infra;
        public LifeAlertService(ILifeAlertInfrastructureService infrastructureService)
        {
            _infra = infrastructureService;
        }

        public async Task Do()
        {
            Console.WriteLine("LifeAlert triggered");
            var updated = false;
            //Gather the entries that need to be announced
            var stati = await _infra.GetStatusesAsync();
            foreach( var nodeState in stati)
            {
                try
                {
                    var announcedState = nodeState.announced;
                    var isStale = nodeState.timestamp.AddMinutes(5) < DateTime.Now;
                    var isAlive = nodeState.online;
                    var email = "vtechdelete+life@gmail.com";
                    if (isAlive & isStale)
                    {
                        _infra.SendMessage(email, "Report", $"{nodeState.name} is offline");
                        nodeState.announced = false;
                        nodeState.online = false;
                        await _infra.UpdateNode(nodeState);
                        updated = true;
                        Console.WriteLine($"{nodeState.name} is offline");
                    }
                    else if (!announcedState & !isStale)
                    {
                        _infra.SendMessage(email, "Report", $"{nodeState.name} is online");
                        nodeState.announced = true;
                        nodeState.online = true;
                        await _infra.UpdateNode(nodeState);
                        updated = true;
                        Console.WriteLine($"{nodeState.name} is online");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
            }
            if(updated)
            {
                Console.WriteLine("Updates sent");
            }
        }
    }
}
