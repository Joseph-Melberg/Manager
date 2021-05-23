using System;
using Inter.Domain;
using Inter.Infrastructure.MySQL.Models;

namespace Inter.Infrastructure.Mappers
{
    public static class PlaneMapper
    {
        public static PlaneModel ToModel(this Plane plane,int now, DateTime time)
        {
            return new PlaneModel
            {
                altitude = plane.altitude,
                date = time,
                flight = plane.flight,
                hex = plane.hexValue,
                lat = plane.lat,
                lon = plane.lon,
                messages = plane.messages,
                now = now,
                nucp = plane.nucp,
                rssi = plane.rssi,
                speed = plane.speed,
                squawk = plane.squawk,
                track = plane.track,
                vert_rate = plane.vert_rate
            };
        }
    }
}