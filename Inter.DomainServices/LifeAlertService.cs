using System;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Microsoft.Extensions.Configuration;

namespace Inter.DomainServices
{
    public class LifeAlertService : ILifeAlertService
    {
        private readonly ILifeAlertInfrastructureService _infra;
        public LifeAlertService(ILifeAlertInfrastructureService infrastructureService)
        {
            _infra = infrastructureService;
        }

        public async void Do()
        {

            //Gather the entries that need to be announced
            var stati = _infra.GetStatuses();
            foreach( var nodeState in stati)
            {
                try
                {


                    var announcedState = nodeState.announced;
                    var isStale = nodeState.timestamp.AddMinutes(2) < DateTime.Now;
                    var isAlive = nodeState.online;

                    Console.WriteLine($"{nodeState.name} is \n announce alive:{announcedState} \n stale:{isStale} \n alive {isAlive}");
                    if (isAlive & isStale)
                    {
                        Console.WriteLine($"{nodeState.name} is dead");
                        _infra.SendMessage("6302478698@txt.att.net", "Report", $"{nodeState.name} is offline");
                        nodeState.announced = false;
                        nodeState.online = false;
                        await _infra.UpdateNode(nodeState);
                    }
                    else if (!announcedState & !isStale)
                    {
                        Console.WriteLine($"{nodeState.name} is alive");
                        _infra.SendMessage("6302478698@txt.att.net", "Report", $"{nodeState.name} is online");
                        nodeState.announced = true;
                        await _infra.UpdateNode(nodeState);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
            Console.Write("done");
        }
    }
}
