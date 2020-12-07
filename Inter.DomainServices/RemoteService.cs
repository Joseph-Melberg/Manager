using System.Threading.Tasks;
using Inter.DomainServices.Core;

namespace Inter.DomainServices
{
    public class RemoteService : IRemoteService
    {
        public RemoteService()
        {
        }

        public Task<bool> Handle(string name, int port)
        {
            if(port == -1)//
            {

            }
            else
            {

            }
            return null;
        }
    }
}
