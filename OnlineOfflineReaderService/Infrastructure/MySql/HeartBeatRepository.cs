using System;
using System.Threading.Tasks;
using System.Linq;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure.MySql
{
    public class HeartBeatRepository : IHeartBeatRepository
    {

        private readonly IHeartBeatContext _heartBeatContext;

        public HeartBeatRepository(IHeartBeatContext heartBeatContext)
        {
            _heartBeatContext = heartBeatContext;
        }

        public async Task UpdateAsync(HeartBeatModel heartBeat)
        {
            
            if(_heartBeatContext.HeartBeat.Any(_ => _.name == heartBeat.name))
            {
                try
                {
                    _heartBeatContext.HeartBeat.Update(heartBeat);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                _heartBeatContext.HeartBeat.Add(heartBeat);
            }
            await _heartBeatContext.Save();
        }
    }
}
