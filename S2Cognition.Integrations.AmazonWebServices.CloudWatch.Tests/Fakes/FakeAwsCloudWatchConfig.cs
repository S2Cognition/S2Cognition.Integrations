﻿using Amazon.CloudWatch;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes
{
    public class FakeAwsCloudWatchConfig : IAwsCloudWatchConfig
    {
        public string? ServiceUrl { get; set; }
        public IAwsRegionEndpoint? RegionEndpoint { get; set; }

        public AmazonCloudWatchConfig Native => throw new NotImplementedException();
    }
}

