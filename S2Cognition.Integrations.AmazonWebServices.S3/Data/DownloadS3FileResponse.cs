namespace S2Cognition.Integrations.AmazonWebServices.S3.Data
{
    public class DownloadS3FileResponse
    {
        public byte[]? FileData { get; set; } = null;
        public string? SignedURL { get; set; } = null;
    }
}
