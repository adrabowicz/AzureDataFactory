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
    public static class Datasets
    {
        public static void CreateDataset(DataFactoryManagementClient client)
        {
            Console.WriteLine("Creating dataset " + Config.BlobDatasetName + "...");
            var blobDataset = new DatasetResource(
                new AzureBlobDataset
                {
                    LinkedServiceName = new LinkedServiceReference
                    {
                        ReferenceName = Config.StorageLinkedServiceName
                    },
                    FolderPath = new Expression { Value = "@{dataset().path}" },
                    Parameters = new Dictionary<string, ParameterSpecification>
                    {
                        { "path", new ParameterSpecification { Type = ParameterType.String } }
                    }
                }
            );
            client.Datasets.CreateOrUpdate(Config.ResourceGroup, Config.DataFactoryName, Config.BlobDatasetName, blobDataset);
            Console.WriteLine(SafeJsonConvert.SerializeObject(blobDataset, client.SerializationSettings));
        }
    }
}
