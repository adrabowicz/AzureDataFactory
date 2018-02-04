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

            Console.WriteLine("Creating linked service " + Config.StorageLinkedServiceName + "...");
            var linkedServiceResource = new LinkedServiceResource(
                new AzureStorageLinkedService
                {
                    ConnectionString = new SecureString("DefaultEndpointsProtocol=https;AccountName=" + Config.StorageAccount + ";AccountKey=" + Config.StorageKey)
                }
            );
            client.LinkedServices.CreateOrUpdate(Config.ResourceGroup, Config.DataFactoryName, Config.StorageLinkedServiceName, linkedServiceResource);
            Console.WriteLine(SafeJsonConvert.SerializeObject(linkedServiceResource, client.SerializationSettings));
        }
    }
}
