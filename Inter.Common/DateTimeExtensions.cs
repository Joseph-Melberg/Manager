using System;

namespace Inter.Common;

public static class DateTimeExtensions
{
    public static DateTime ClipSubSecond(this DateTime date) =>
        new DateTime(
            date.Year,
            date.Month,
            date.Day,
            date.Hour,
            date.Minute, 
            date.Second);
}