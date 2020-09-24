﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatContext
    {
        DbSet<HeartBeatModel> HeartBeat { get; set; }
        Task Save();
    }
}
