using System;

namespace InterApi.ServiceModels
{
    public class NodeStatusResponse
    {
        public string Name {get; set;}

        public bool Online {get; set;}

        public DateTime LastHeartbeat {get; set;}
    }
}