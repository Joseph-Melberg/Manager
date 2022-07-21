using System;

namespace Inter.Common.Clock;

public class Clock : IClock
{
    public DateTime GetUtcNow() => DateTime.UtcNow;
}