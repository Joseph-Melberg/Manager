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
                    }
                    else if (!announcedState & !isStale)
                    {
                        _infra.SendMessage(email, "Report", $"{nodeState.name} is online");
                        nodeState.announced = true;
                        nodeState.online = true;
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
