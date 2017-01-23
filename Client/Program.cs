using Contracts;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Client;
using System;
using System.Fabric;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
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
            ServicePartitionResolver servicePartitionResolver = new ServicePartitionResolver(() => new FabricClient());
            WcfCommunicationClientFactory<IPresidentialService> communicationClientFactory = new WcfCommunicationClientFactory<IPresidentialService>(binding, servicePartitionResolver: servicePartitionResolver);

            Uri uri = new Uri("fabric:/Politics/PresidentialService");


            PresidentialServiceClient presidentialServiceClient = new PresidentialServiceClient(communicationClientFactory, uri);


            //v2.0.0
            //while (true)
            //{
                int presidentId = 44;
                string presidentName = presidentialServiceClient.PresidentName(presidentId).Result;
                Console.WriteLine("{0}th president is {1}", presidentId, presidentName);
                string presidents = presidentialServiceClient.Presidents().Result;
                Console.WriteLine("Last 5 presidents {0}", presidents);
            Console.ReadLine();//remove for v2.0.0
            //    Console.WriteLine();
            //    Thread.Sleep(1000);
            //}
        }
    }
}
