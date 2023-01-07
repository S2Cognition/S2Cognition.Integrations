using Oracle.NetSuite;

namespace S2Cognition.Integrations.NetSuite.Core.Models;

public interface INetSuiteService
{
    Task<getResponse?> Get(BaseRef record);
    Task<getListResponse?> List(params BaseRef[] records);
    Task<addResponse?> Add(Record record);
    Task<updateResponse?> Update(Record record);
}

public class NetSuiteService : INetSuiteService
{
    internal NetSuiteService()
    {
    }
}
