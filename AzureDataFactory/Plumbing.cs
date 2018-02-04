using System;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ADFv2QuickStart
{
    public static class Plumbing
    {
        public static TokenCredentials Authenticate()
        {
            var context = new AuthenticationContext("https://login.windows.net/" + Config.tenantID);
            var clientCredentials = new ClientCredential(Config.applicationId, Config.authenticationKey);
            var result = context.AcquireTokenAsync("https://management.azure.com/", clientCredentials).Result;
            var serviceClientCredentials = new TokenCredentials(result.AccessToken);
            return serviceClientCredentials;
        }

        public static void CreateDataFactory(DataFactoryManagementClient client)
        {
            Console.WriteLine("Creating data factory " + Config.dataFactoryName + "...");
            var dataFactory = new Factory
            {
                Location = Config.region,
                Identity = new FactoryIdentity()
            };
            client.Factories.CreateOrUpdate(Config.resourceGroup, Config.dataFactoryName, dataFactory);
            Console.WriteLine(SafeJsonConvert.SerializeObject(dataFactory, client.SerializationSettings));

            while (client.Factories.Get(Config.resourceGroup, Config.dataFactoryName).ProvisioningState == "PendingCreation")
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

    }
}
