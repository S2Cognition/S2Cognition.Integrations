namespace S2Cognition.Integrations.AmazonWebServices.SSM.Data
{
    public class PutSSMParameterRequest
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}
