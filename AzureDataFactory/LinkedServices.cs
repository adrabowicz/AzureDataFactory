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
        public static void CreateLinkedServiceResource(DataFactoryManagementClient client)
        {

            Console.WriteLine("Creating linked service " + Config.storageLinkedServiceName + "...");
            var linkedServiceResource = new LinkedServiceResource(
                new AzureStorageLinkedService
                {
                    ConnectionString = new SecureString("DefaultEndpointsProtocol=https;AccountName=" + Config.storageAccount + ";AccountKey=" + Config.storageKey)
                }
            );
            client.LinkedServices.CreateOrUpdate(Config.resourceGroup, Config.dataFactoryName, Config.storageLinkedServiceName, linkedServiceResource);
            Console.WriteLine(SafeJsonConvert.SerializeObject(linkedServiceResource, client.SerializationSettings));
        }
    }
}
