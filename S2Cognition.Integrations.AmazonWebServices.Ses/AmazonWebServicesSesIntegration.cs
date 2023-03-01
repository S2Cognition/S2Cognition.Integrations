using Amazon.SimpleEmailV2.Model;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Ses.Data;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;
using S2Cognition.Integrations.Core;
using System.Text.RegularExpressions;

namespace S2Cognition.Integrations.AmazonWebServices.Ses;

public interface IAmazonWebServicesSesIntegration : IIntegration<AmazonWebServicesSesConfiguration>
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="req">
    /// The SendRequest.
    /// 
    /// Requires a meaningful Sender, Recipient, Subject, and Body.
    /// </param>
    /// <returns>SendResponse</returns>
    Task<SendResponse> Send(SendRequest req);
}

public partial class AmazonWebServicesSesIntegration : Integration<AmazonWebServicesSesConfiguration>, IAmazonWebServicesSesIntegration
{
    [GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])")]
    private static partial Regex EmailAddressRegex();

    private IAwsSesClient? _client;
    private IAwsSesClient Client
    {
        get
        {
            if (_client == null)
            {
                var factory = _ioc.GetRequiredService<IAwsSesClientFactory>();
                var regionFactory = _ioc.GetRequiredService<IAwsRegionFactory>();

                _client = factory.Create(new AwsSesConfig
                {
                    RegionEndpoint = regionFactory.Create(Configuration.AwsRegion).Result
                });
            }

            return _client;
        }
    }
    internal AmazonWebServicesSesIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public async Task<SendResponse> Send(SendRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Sender) || !EmailAddressRegex().IsMatch(req.Sender))
            throw new ArgumentException(nameof(SendRequest.Sender));

        if (string.IsNullOrWhiteSpace(req.Recipient) || !EmailAddressRegex().IsMatch(req.Recipient))
            throw new ArgumentException(nameof(SendRequest.Recipient));

        if (string.IsNullOrWhiteSpace(req.Subject))
            throw new ArgumentException(nameof(SendRequest.Subject));

        if (string.IsNullOrWhiteSpace(req.Body))
            throw new ArgumentException(nameof(SendRequest.Body));

        var toAddresses = new List<string>();
        if (!string.IsNullOrWhiteSpace(req.Recipient))
            toAddresses.Add(req.Recipient);

        var sesRequest = new SendEmailRequest
        {
            FromEmailAddress = req.Sender,
            Destination = new Destination
            {
                ToAddresses = toAddresses
            },
            Content = new EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content { Data = req.Subject },
                    Body = new Body { Html = new Content { Data = req.Body } }
                }
            }
        };

        await Client.Send(sesRequest);

        return new SendResponse();
    }
}
