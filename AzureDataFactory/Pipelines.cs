using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;

namespace ADFv2QuickStart
{
    public static class Pipelines
    {
        public static void CreateBlobPipeline(DataFactoryManagementClient client, string dataFactoryName, string blobDatasetName, string pipelineName)
        {
            var pipeline = new PipelineResource
            {
                Parameters = new Dictionary<string, ParameterSpecification>
                {
                    { "inputPath", new ParameterSpecification { Type = ParameterType.String } },
                    { "outputPath", new ParameterSpecification { Type = ParameterType.String } }
                },
                Activities = Activities.CreateBlobActivities(blobDatasetName)
            };
            client.Pipelines.CreateOrUpdate(Config.ResourceGroup, dataFactoryName, pipelineName, pipeline);
            Console.WriteLine(SafeJsonConvert.SerializeObject(pipeline, client.SerializationSettings));
        }

        public static void CreateHttpPipeline(DataFactoryManagementClient client, string dataFactoryName, string httpDatasetName, string pipelineName)
        {
            var pipeline = new PipelineResource
            {
                Activities = Activities.CreateHttpActivities(httpDatasetName, "")
            };
            client.Pipelines.CreateOrUpdate(Config.ResourceGroup, dataFactoryName, pipelineName, pipeline);
            Console.WriteLine(SafeJsonConvert.SerializeObject(pipeline, client.SerializationSettings));
        }

        public static void RunPipeline(DataFactoryManagementClient client, string dataFactoryName, string pipelineName, string inputBlobPath, string outputBlobPath)
        {
            Console.WriteLine("Creating pipeline " + pipelineName + "...");

            Console.WriteLine("Creating pipeline run...");
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "inputPath", inputBlobPath },
                { "outputPath", outputBlobPath }
            };
            var runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(Config.ResourceGroup, dataFactoryName, pipelineName, parameters).Result.Body;
            Console.WriteLine("Pipeline run ID: " + runResponse.RunId);

            Console.WriteLine("Checking pipeline run status...");
            PipelineRun pipelineRun;
            while (true)
            {
                pipelineRun = client.PipelineRuns.Get(Config.ResourceGroup, dataFactoryName, runResponse.RunId);
                Console.WriteLine("Status: " + pipelineRun.Status);
                if (pipelineRun.Status == "InProgress")
                    System.Threading.Thread.Sleep(15000);
                else
                    break;
            }

            Console.WriteLine("Checking copy activity run details...");

            var activityRuns = client.ActivityRuns.ListByPipelineRun(
                                Config.ResourceGroup, dataFactoryName, runResponse.RunId, DateTime.UtcNow.AddMinutes(-10),
                                DateTime.UtcNow.AddMinutes(10)).ToList();
            if (pipelineRun.Status == "Succeeded")
            {
                Console.WriteLine(activityRuns.First().Output);
            }
            else
            {
                Console.WriteLine(activityRuns.First().Error);
            }
        }
    }
}
