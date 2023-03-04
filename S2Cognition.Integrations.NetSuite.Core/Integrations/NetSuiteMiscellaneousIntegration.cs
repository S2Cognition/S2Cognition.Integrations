using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;

using SystemTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

public interface INetSuiteMiscellaneousIntegration
{
    Task<GetCustomRecordResponse> GetCustomRecord(GetCustomRecordRequest request);
    Task<CreateNoteResponse> CreateNote(CreateNoteRequest request);
}

internal class NetSuiteMiscellaneousIntegration : NetSuiteSubIntegrationBase, INetSuiteMiscellaneousIntegration
{
    public NetSuiteMiscellaneousIntegration(IServiceProvider serviceProvider, NetSuiteIntegration parent)
        : base(serviceProvider, parent)
    {
    }

    public async Task<CreateNoteResponse> CreateNote(CreateNoteRequest reqest)
    {
        /*
                public async Task<CreateNoteResponse> CreateNote(CreateNoteRequest req)
                {
                    var client = await BuildClient();

                    var response = await client.Add(new Note
                    {
                        title = req.Title,
                        note = req.Note,
                        noteDate = DateTime.UtcNow,
                        noteDateSpecified = true,
                        entity = new RecordRef
                        {
                            externalId = req.ExternalId
                        }
                    });

                    return ProcessResponse(new CreateNoteResponse(), response);
                }
            */
        await SystemTask.CompletedTask;
        throw new InvalidOperationException();
    }

    public async Task<GetCustomRecordResponse> GetCustomRecord(GetCustomRecordRequest request)
    {
        var client = await BuildClient();

        var options = new CustomRecordSearch
        {
            basic = new CustomRecordSearchBasic
            {
                recType = new RecordRef { internalId = request.InternalId }
            }
        };

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        if (response == null)
            throw new InvalidOperationException();

        var customRecord = response.searchResult.recordList
            .OfType<CustomRecord>()
            .Select(record => new
            {
            });

        return new GetCustomRecordResponse
        {
            //Results = customRecord.ToList()
        };
    }
}
