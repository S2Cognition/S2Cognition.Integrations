namespace S2Cognition.Integrations.AmazonWebServices.S3.Data
{
    public class UploadS3FileRequest
    {
        public byte[]? FileData { get; set; } = null;
        public string? BucketName { get; set; }
        public string? FileName { get; set; }

    }
}
