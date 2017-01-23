using System;
using System.Collections.Generic;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Threading.Tasks;
using DataStore;
using System.ServiceModel;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Contracts;

namespace PresidentialService
{
    internal sealed class PresidentialService : StatelessService, IPresidentialService
    {
        private PresidentialDataStore _dataStore;
        public PresidentialService(StatelessServiceContext context)
            : base(context)
        {

            _dataStore = new PresidentialDataStore();
        }

        public Task<string> PresidentName(int id)
        {
            return Task.FromResult<string>(
                    string.Format("Node: {0} | PresidentName:{1}", this.Context.NodeContext.NodeName, _dataStore.GetPresidents()[id]
                )
            );
        }

        public Task<string> Presidents()
        {
            return Task.FromResult<string>(
                    string.Format("Node: {0} | Presidents:{1}", this.Context.NodeContext.NodeName, string.Join(", ",_dataStore.GetPresidents().Values)
                )
            );
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            const int bufferSize = 512000; //500KB
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None)
            {
                SendTimeout = TimeSpan.FromSeconds(30),
                ReceiveTimeout = TimeSpan.FromSeconds(30),
                CloseTimeout = TimeSpan.FromSeconds(30),
                MaxConnections = 1000,
                MaxReceivedMessageSize = bufferSize,
                MaxBufferSize = bufferSize,
                MaxBufferPoolSize = bufferSize * Environment.ProcessorCount,
            };

            ServiceInstanceListener listener = new ServiceInstanceListener(context =>
                    new WcfCommunicationListener<IPresidentialService>(context, this, binding, "ServiceEndpoint")
                );

            return new[] {
                    listener
            };
        }
    }
}
