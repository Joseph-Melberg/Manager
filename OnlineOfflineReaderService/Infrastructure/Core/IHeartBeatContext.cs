using System;
using Microsoft.EntityFrameworkCore;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatContext
    {
        DbSet<HeartBeatModel> HeartBeats { get; set; }
    }
}
