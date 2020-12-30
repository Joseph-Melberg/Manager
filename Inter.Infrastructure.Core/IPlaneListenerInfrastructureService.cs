using System;

namespace Inter.Infrastructure.Core
{
    public interface IPlaneListenerInfrastructureService
    {
        void AddRecentICAOAddr(UInt32 address);
    }
}