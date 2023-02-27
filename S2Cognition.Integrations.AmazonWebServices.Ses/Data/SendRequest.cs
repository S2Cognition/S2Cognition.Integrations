namespace S2Cognition.Integrations.AmazonWebServices.Ses.Data;
public class SendRequest
{
    public string? Sender { get; set; }
    public string? Recipient { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
