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
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=" + Config.StorageAccount + ";AccountKey=" + Config.StorageKey;
            var properties = new AzureStorageLinkedService
            {
                ConnectionString = new SecureString(connectionString)
            };
            CreateLinkedServiceResource(client, dataFactoryName, linkedServiceName, properties);
        }

        public static void CreateHttpLinkedServiceResource(DataFactoryManagementClient client, string dataFactoryName, string linkedServiceName, string baseUrl)
        {
            var properties = new HttpLinkedService
            {
                AuthenticationType = "Anonymous",
                Url = baseUrl,   // base url of the web server
                EnableServerCertificateValidation = false
            };
            CreateLinkedServiceResource(client, dataFactoryName, linkedServiceName, properties);
        }

        private static void CreateLinkedServiceResource(DataFactoryManagementClient client, string dataFactoryName, string linkedServiceName, LinkedService properties)
        {
            Console.WriteLine("Creating linked service " + linkedServiceName + "...");
            var linkedServiceResource = new LinkedServiceResource(properties);
            client.LinkedServices.CreateOrUpdate(Config.ResourceGroup, dataFactoryName, linkedServiceName, linkedServiceResource);
            Console.WriteLine(SafeJsonConvert.SerializeObject(linkedServiceResource, client.SerializationSettings));
        }
    }
}
