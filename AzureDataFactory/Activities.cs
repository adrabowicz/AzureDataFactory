using System.Collections.Generic;
using Microsoft.Azure.Management.DataFactory.Models;

namespace ADFv2QuickStart
{
    public static class Activities
    {
        public static List<Activity> CreateActivities(string blobDatasetName)
        {
            var activities = new List<Activity>
                {
                    new CopyActivity
                    {
                        Name = "CopyFromBlobToBlob",
                        Inputs = new List<DatasetReference>
                        {
                            new DatasetReference()
                            {
                                ReferenceName = blobDatasetName,
                                Parameters = new Dictionary<string, object>
                                {
                                    { "path", "@pipeline().parameters.inputPath" }
                                }
                            }
                        },
                        Outputs = new List<DatasetReference>
                        {
                            new DatasetReference
                            {
                                ReferenceName = blobDatasetName,
                                Parameters = new Dictionary<string, object>
                                {
                                    { "path", "@pipeline().parameters.outputPath" }
                                }
                            }
                        },
                        Source = new BlobSource { },
                        Sink = new BlobSink { }
                    }
                };
            return activities;
        }
    }
}
