﻿using System;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;

namespace Inter.DomainServices
{
    public class LifeAlertService : ILifeAlertService
    {
        private readonly ILifeAlertInfrastructureService _infra;
        private readonly ILifeAlertRateConfiguration _rateConfig;
        private readonly IEmailRecipientConfiguration _emailConfig;
        public LifeAlertService(
            ILifeAlertInfrastructureService infrastructureService,
            ILifeAlertRateConfiguration rateConfiguration,
            IEmailRecipientConfiguration emailRecipientConfiguration)
        {
            _infra = infrastructureService;
            _rateConfig = rateConfiguration;
            _emailConfig = emailRecipientConfiguration;
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
                    var isStale = IsStale(nodeState);
                    var isAlive = nodeState.online;
                    if (isAlive && isStale)
                    {
                        await UpdateAndAnnounceDeadNodeAsync(nodeState);
                        updated = true;
                    }
                    else if (!announcedState && !isStale)
                    {
                        await UpdateAndAnnounceLiveNodeAsync(nodeState);
                        updated = true;
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
        private async Task UpdateAndAnnounceDeadNodeAsync(Heartbeat nodeState)
        {
            _infra.SendMessage(_emailConfig.Recipient, "Report", $"{nodeState.name} is offline");
            nodeState.announced = false;
            nodeState.online = false;
            await _infra.UpdateNodeAsync(nodeState);
            Console.WriteLine($"{nodeState.name} is offline");
        }
        private async Task UpdateAndAnnounceLiveNodeAsync(Heartbeat nodeState)
        {
            _infra.SendMessage(_emailConfig.Recipient, "Report", $"{nodeState.name} is online");
            nodeState.announced = true;
            nodeState.online = true;
            await _infra.UpdateNodeAsync(nodeState);
            Console.WriteLine($"{nodeState.name} is online");
        }
        private bool IsStale(Heartbeat nodeState) => nodeState.timestamp.AddMinutes(_rateConfig.Rate) < DateTime.Now;
    }
}
