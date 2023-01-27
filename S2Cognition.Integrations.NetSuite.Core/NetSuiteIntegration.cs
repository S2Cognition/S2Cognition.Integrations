using S2Cognition.Integrations.Core;
using S2Cognition.Integrations.NetSuite.Core.Data;
using S2Cognition.Integrations.NetSuite.Core.Integrations;

namespace S2Cognition.Integrations.NetSuite.Core;

public interface INetSuiteIntegration : IIntegration<NetSuiteConfiguration>
{
    INetSuiteCustomerEntitiesIntegration CustomerEntities { get; }
    INetSuiteLeadsIntegration Leads { get; }
    INetSuiteProspectsIntegration Prospects { get; }
    INetSuiteCustomersIntegration Customers { get; }
    INetSuiteMiscellaneousIntegration Miscellaneous { get; }
}

internal class NetSuiteIntegration : Integration<NetSuiteConfiguration>, INetSuiteIntegration
{
    public INetSuiteCustomerEntitiesIntegration CustomerEntities { get; private set;}
    public INetSuiteLeadsIntegration Leads { get; private set;}
    public INetSuiteProspectsIntegration Prospects { get; private set;}
    public INetSuiteCustomersIntegration Customers { get; private set;}
    public INetSuiteMiscellaneousIntegration Miscellaneous { get; private set; }


    public NetSuiteIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        CustomerEntities = new NetSuiteCustomerEntitiesIntegration(serviceProvider, this);
        Leads = new NetSuiteLeadsIntegration(serviceProvider, this);
        Prospects = new NetSuiteProspectsIntegration(serviceProvider, this);
        Customers = new NetSuiteCustomersIntegration(serviceProvider, this);
        Miscellaneous = new NetSuiteMiscellaneousIntegration(serviceProvider, this);
    }
}
