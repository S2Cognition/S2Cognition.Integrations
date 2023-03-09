using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.S3;

public interface IAmazonWebServicesS3Integration : IIntegration<AmazonWebServicesS3Configuration>
{
    /// <summary>
    /// Downloads a file from S3.
    /// </summary>
    /// <param name="req">
    /// The DownloadS3FileRequest.
    /// 
    /// Must have a meaningful BucketName and FileName.
    /// </param>
    /// <returns>DownloadS3FileResponse</returns>
    Task<DownloadS3FileResponse> DownloadS3File(DownloadS3FileRequest req);

    /// <summary>
    /// Uploads a file to S3.
    /// </summary>
    /// <param name="req">
    /// The UploadS3FileRequest.
    /// 
    /// Must have a meaningful BucketName, FileName, and FileData.
    /// </param>
    /// <returns>UploadS3FileResponse</returns>
    Task<UploadS3FileResponse> UploadS3File(UploadS3FileRequest req);
}

internal class AmazonWebServicesS3Integration : Integration<AmazonWebServicesS3Configuration>, IAmazonWebServicesS3Integration
{
    private IAwsS3Client? _client;
    private IAwsS3Client Client
    {
        get
        {
            if (_client == null)
            {
                var factory = _serviceProvider.GetRequiredService<IAwsS3ClientFactory>();
                var regionFactory = _serviceProvider.GetRequiredService<IAwsRegionFactory>();

                _client = factory.Create(new AwsS3Config
                {
                    ServiceUrl = Configuration.ServiceUrl,
                    RegionEndpoint = regionFactory.Create(Configuration.AwsRegion).Result
                });
            }

            return _client;
        }
    }

    internal AmazonWebServicesS3Integration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public async Task<DownloadS3FileResponse> DownloadS3File(DownloadS3FileRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.BucketName))
            throw new ArgumentException(nameof(DownloadS3FileRequest.BucketName));

        if (String.IsNullOrWhiteSpace(req.FileName))
            throw new ArgumentException(nameof(DownloadS3FileRequest.FileName));

        return await Client.DownloadFileAsync(req);
    }

    public async Task<UploadS3FileResponse> UploadS3File(UploadS3FileRequest req)
    {
        if ((req.FileData == null) || (req.FileData.Length < 1))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileData));

        if (String.IsNullOrWhiteSpace(req.BucketName))
            throw new ArgumentException(nameof(UploadS3FileRequest.BucketName));

        if (String.IsNullOrWhiteSpace(req.FileName))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileName));

        return await Client.UploadFileAsync(req);

    }
}


