using System;
using System.Linq;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;

namespace ADFv2QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void Run()
        {
            var clientCredentials = Plumbing.Authenticate();
            var client = new DataFactoryManagementClient(clientCredentials) { SubscriptionId = Config.SubscriptionId };
            // Plumbing.CreateDataFactory(client);
            LinkedServices.CreateLinkedServiceResource(client);
            Datasets.CreateDataset(client);
            Pipelines.CreatePipeline(client);
            Pipelines.RunPipeline(client);

            //Console.WriteLine("Deleting the data factory");
            //client.Factories.Delete(resourceGroup, dataFactoryName);
        }
    }
}
