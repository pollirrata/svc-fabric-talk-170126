using Contracts;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using System;
using System.Threading.Tasks;

namespace Client
{
    public class PresidentialServiceClient : ServicePartitionClient<WcfCommunicationClient<IPresidentialService>>
    {
        public PresidentialServiceClient(WcfCommunicationClientFactory<IPresidentialService> clientFactory, Uri serviceName)
            : base(clientFactory, serviceName)
        { }
        public Task<string> PresidentName(int id)
        {
            return this.InvokeWithRetryAsync(client => client.Channel.PresidentName(id));
        }

        public Task<string> Presidents()
        {
            return this.InvokeWithRetryAsync(client => client.Channel.Presidents());
        }
    }
}
