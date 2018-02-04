using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;

namespace ADFv2QuickStart
{
    public static class Datasets
    {
        public static void CreateBlobStorageDataset(DataFactoryManagementClient client, string dataFactoryName, string linkedServiceName, string blobDatasetName)
        {
            var properties = new AzureBlobDataset
            {
                LinkedServiceName = new LinkedServiceReference
                {
                    ReferenceName = linkedServiceName
                },
                FolderPath = new Expression
                {
                    Value = "@{dataset().path}"
                },
                Parameters = new Dictionary<string, ParameterSpecification>
                {
                    { "path", new ParameterSpecification { Type = ParameterType.String } }
                }
            };
            CreateDataset(client, dataFactoryName, blobDatasetName, properties);
        }

        public static void CreateHttpFileDataset(DataFactoryManagementClient client, string dataFactoryName, string linkedServiceName, 
                                                    string blobDatasetName, string relativeUrl)
        {
            var properties = new HttpDataset // a file from http web server
            {
                // A relative URL to the resource that contains the data. 
                // When this property is not specified, only the URL specified in the linked service definition is used.
                RelativeUrl = relativeUrl,
                RequestMethod = "Get"
            };
            CreateDataset(client, dataFactoryName, blobDatasetName, properties);
        }

        private static void CreateDataset(DataFactoryManagementClient client, string dataFactoryName, string blobDatasetName, Dataset properties)
        {
            Console.WriteLine("Creating dataset " + blobDatasetName + "...");
            var blobDataset = new DatasetResource(properties);
            client.Datasets.CreateOrUpdate(Config.ResourceGroup, dataFactoryName, blobDatasetName, blobDataset);
            Console.WriteLine(SafeJsonConvert.SerializeObject(blobDataset, client.SerializationSettings));
        }
    }
}
