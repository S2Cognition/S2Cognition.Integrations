namespace S2Cognition.Integrations.AmazonWebServices.S3.Data
{
    public class DownloadS3FileRequest
    {
        public string? BucketName { get; set; } = null;
        public string? FileName { get; set; } = null;
    }
}
