using Amazon;
using Amazon.CloudWatchLogs;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public interface IAmazonWebServicesIntegration : IIntegration<AmazonWebServicesConfiguration>
{
}

internal class AmazonWebServicesIntegration : Integration<AmazonWebServicesConfiguration>, IAmazonWebServicesIntegration
{
    private AmazonCloudWatchLogsClient CloudWatchLogsClient
    {
        get
        {
            var region = RegionEndpoint.GetBySystemName(Configuration.AwsRegion);

            var cloudwatchConfig = new AmazonCloudWatchLogsConfig
            {
                RegionEndpoint = region
            };

            return new AmazonCloudWatchLogsClient(Configuration.AccessKey, Configuration.SecretKey, cloudwatchConfig);
        }
    }

    public void GetSomething()
    {
    }
}
/*
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Microsoft.Extensions.Logging;
using S2Cognition.WebApi.Logic.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using S2Cognition.DAL.Core.Data;
using S2Cognition.ConfigurationService.Data;

namespace S2Cognition.WebApi.Logic
{
public interface ICloudWatchLogic : IBusinessLogic
{
}
public class CloudWatchLogic : BusinessLogic, ICloudWatchLogic
{
    private AmazonCloudWatchLogsClient _cloudwatch;
    private readonly int _eventLimit = 100;
    private readonly int _logStreamLimit = 10;
    private readonly int _defaultNumberofRequestedEvents = 50;
    private readonly int _maxLookupsFromAws = 3;

    public CloudWatchLogic(IServiceProvider di)
        : base(di)
    {
        InitLogger<CloudWatchLogic>();
    }

    public async Task<IList<LogRecord>> GetLogMessages(string logGroupName)
    {
        if (logGroupName == null || logGroupName == "")
            logGroupName = _cfg.Get(ConfigurationKey.AwsSection, ConfigurationKey.AwsCloudWatchLogGroupWebApi); ;


        _cloudwatch = CreateNewCloudWatchClient();

        var logRecords = new List<LogRecord>();


        if (_cloudwatch != null)
        {
            var counter = 0;
            var findMoreLogs = true;
            string accessToken = null;

            while (findMoreLogs)
            {
                var fetchedData = await RetrieveCloudWatchLogs(logGroupName, _defaultNumberofRequestedEvents, accessToken);
                logRecords.AddRange(fetchedData.data);
                accessToken = fetchedData.accessToken;

                counter++;
                if (counter > _maxLookupsFromAws || logRecords.Count >= _eventLimit)
                    findMoreLogs = false;

                if (logRecords.Count > _eventLimit)
                    logRecords = logRecords.GetRange(0, _eventLimit);
            }

            return logRecords;
        }
        return logRecords;
    }

    private async Task<(string accessToken, IList<LogRecord> data)> RetrieveCloudWatchLogs(string logGroupName, int requestedNumberOfEvents, string nextToken = null)
    {
        var logMessages = new List<LogRecord>();
        var accessToken = "";
        var describeLogStreamsResponse = await GetLogStreamResponse(logGroupName, nextToken);
        if (describeLogStreamsResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            accessToken = describeLogStreamsResponse.NextToken;

            foreach (var logStream in describeLogStreamsResponse.LogStreams)
            {
                var getLogEventsRequest = CreateGetLogEventsRequest(logGroupName, logStream.LogStreamName);

                var eventsResponse = await _cloudwatch.GetLogEventsAsync(getLogEventsRequest);

                if (eventsResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (var evnt in eventsResponse.Events)
                    {
                        var logRecord = ConvertToLogRecord(evnt);
                        logMessages.Add(logRecord);
                    }
                }
            }
            return (accessToken, logMessages);
        }
        return (accessToken, logMessages);
    }

    private LogRecord ConvertToLogRecord(OutputLogEvent evnt)
    {
        return new LogRecord
        {
            Message = evnt.Message,
            CreatedOn = evnt.Timestamp
        };
    }

    private async Task<DescribeLogStreamsResponse> GetLogStreamResponse(string logGroupName, string nextToken)
    {

        var logStreamsRequestDesc = CreateDescribeLogStreamsRequest(logGroupName, nextToken);

        try
        {
            return await _cloudwatch.DescribeLogStreamsAsync(logStreamsRequestDesc);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message, e.InnerException);
            return null;
        }

    }

    private GetLogEventsRequest CreateGetLogEventsRequest(string logGroupName, string logStreamName, string token = null)
    {
        var request = new GetLogEventsRequest()
        {
            LogStreamName = logStreamName,
            LogGroupName = logGroupName,
            Limit = _eventLimit
        };

        if (token != null)
        {
            request.NextToken = token;
        }

        return request;
    }

    private DescribeLogStreamsRequest CreateDescribeLogStreamsRequest(string logGroupName, string nextToken)
    {
        var request = new DescribeLogStreamsRequest
        {
            LogGroupName = logGroupName,
            Descending = true,
            OrderBy = OrderBy.LastEventTime,
            Limit = _logStreamLimit,
            NextToken = null
        };

        if (nextToken != null)
        {
            request.NextToken = nextToken;
        }

        return request;
    }

    private AmazonCloudWatchLogsClient CreateNewCloudWatchClient()
    {
        var accessKey = _cfg.Get(ConfigurationKey.AwsSection, ConfigurationKey.AwsAccessKeyLogger);
        var secretKey = _cfg.Get(ConfigurationKey.AwsSection, ConfigurationKey.AwsSecretKeyLogger);
        var awsRegionConfig = _cfg.Get(ConfigurationKey.AwsSection, ConfigurationKey.AwsRegionLogger);
        var region = RegionEndpoint.GetBySystemName(awsRegionConfig);

        var cloudwatchConfig = new AmazonCloudWatchLogsConfig
        {
            RegionEndpoint = region
        };
        return new AmazonCloudWatchLogsClient(accessKey, secretKey, cloudwatchConfig);
    }
}
}
*/
