using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazanWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.S3;

public interface IAmazonWebServicesS3Integration : IIntegration<AmazonWebServicesS3Configuration>
{
    Task<byte[]> DownloadS3File(DownloadS3FileRequest req);
    Task<bool> UploadS3File(UploadS3FileRequest req);
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
                var factory = _ioc.GetRequiredService<IAwsS3ClientFactory>();

                var regionFactory = _ioc.GetRequiredService<IAwsRegionFactory>();

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

    public async Task<byte[]> DownloadS3File(DownloadS3FileRequest req)
    {

        if (req.BucketName == null ||
            req.FileName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        var response = await Client.DownloadFileAsync(req);
        //{
        //    BucketName = req.BucketName,
        //    FileName = req.FileName
        //});

        if (response != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                response.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
        else
        {
            return Array.Empty<byte>();
        }



    }

    public async Task<bool> UploadS3File(UploadS3FileRequest req)
    {

        if (req.BucketName == null ||
            req.FileName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        var response = await Client.UploadFileAsync(req);

        //if (response)
        //{
        return true;
        //}


    }
}
