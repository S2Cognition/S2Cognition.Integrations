using System.ComponentModel;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Data
{
    public enum EnvironmentType
    {
        [Description("Development")]
        Development,
        [Description("Staging")]
        Staging,
        [Description("Production")]
        Production
    }
}
