namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Data
{
    public class PutSsmParameterRequest
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}
