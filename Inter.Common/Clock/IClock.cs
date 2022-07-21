using System;

namespace Inter.Common.Clock;

public interface IClock
{
    DateTime GetUtcNow();
}