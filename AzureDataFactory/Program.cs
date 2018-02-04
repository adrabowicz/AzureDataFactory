using System;
using Microsoft.Azure.Management.DataFactory;

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
            var pipelineName = "CopyFromFolderToFolder";
            var inputPath = "adftutorial/input";
            var outputPath = "firsttest";

            var clientCredentials = Plumbing.Authenticate();
            var client = new DataFactoryManagementClient(clientCredentials) { SubscriptionId = Config.SubscriptionId };
            LinkedServices.CreateStorageLinkedServiceResource(client, dataFactoryName, linkedServiceName);
            Datasets.CreateBlobStorageDataset(client, dataFactoryName, linkedServiceName, datasetName);
            Pipelines.CreateBlobPipeline(client, dataFactoryName, datasetName, pipelineName);
            Pipelines.RunPipeline(client, dataFactoryName, pipelineName, inputPath, outputPath);
        }

        private static void CopyFormHttpEndpoint()
        {
            var dataFactoryName = "DevDataFactory1";
            var linkedServiceName = "AzureHttpLinkedService";
            var baseUrl = "";
            var datasetName = "HttpFileDataset";
            var relativeUrl = "";
            var pipelineName = "CopyFromHttpEndpoint";

            var clientCredentials = Plumbing.Authenticate();
            var client = new DataFactoryManagementClient(clientCredentials) { SubscriptionId = Config.SubscriptionId };
            LinkedServices.CreateHttpLinkedServiceResource(client, dataFactoryName, linkedServiceName, baseUrl);
            Datasets.CreateHttpFileDataset(client, dataFactoryName, linkedServiceName, datasetName, relativeUrl);
            Pipelines.CreateHttpPipeline(client, dataFactoryName, datasetName, pipelineName);
          //  Pipelines.RunPipeline(client, dataFactoryName, pipelineName, inputPath, outputPath);
        }
    }
}
