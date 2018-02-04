using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ADFv2QuickStart
{
    public static class LinkedServices
    {
        public static void CreateStorageLinkedServiceResource(DataFactoryManagementClient client, string dataFactoryName, string linkedServiceName)
        {

            Console.WriteLine("Creating linked service " + linkedServiceName + "...");
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=" + Config.StorageAccount + ";AccountKey=" + Config.StorageKey;
            var properties = new AzureStorageLinkedService
            {
                ConnectionString = new SecureString(connectionString)
            };
            var linkedServiceResource = new LinkedServiceResource(properties);
            client.LinkedServices.CreateOrUpdate(Config.ResourceGroup, dataFactoryName, linkedServiceName, linkedServiceResource);
            Console.WriteLine(SafeJsonConvert.SerializeObject(linkedServiceResource, client.SerializationSettings));
        }
    }
}
