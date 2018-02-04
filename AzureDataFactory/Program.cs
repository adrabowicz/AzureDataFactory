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
                CopyFormBlobFolderToBlobFolder();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void CopyFormBlobFolderToBlobFolder()
        {
            var dataFactoryName = "DevDataFactory1";
            var linkedServiceName = "AzureStorageLinkedService";
            var datasetName = "BlobDataset";
            var pipelineName = "CopyFormFolderToFolder";
            var inputPath = "adftutorial/input";
            var outputPath = "firsttest";

            var clientCredentials = Plumbing.Authenticate();
            var client = new DataFactoryManagementClient(clientCredentials) { SubscriptionId = Config.SubscriptionId };
            LinkedServices.CreateStorageLinkedServiceResource(client, dataFactoryName, linkedServiceName);
            Datasets.CreateBlobDataset(client, dataFactoryName, linkedServiceName, datasetName);
            Pipelines.CreatePipeline(client, dataFactoryName, datasetName, pipelineName);
            Pipelines.RunPipeline(client, dataFactoryName, pipelineName, inputPath, outputPath);
        }
    }
}
