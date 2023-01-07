namespace S2Cognition.Integrations.NetSuite.Core.Models
{
    public interface INetSuiteServiceFactory
    {
        Task<INetSuiteService> Create();
    }

    public class NetSuiteServiceFactory : INetSuiteServiceFactory
    {
        internal NetSuiteServiceFactory()
        {
        }

        public async Task<INetSuiteService> Create()
        {
            return await Task.FromResult(new NetSuiteService());
        }
    }
}
