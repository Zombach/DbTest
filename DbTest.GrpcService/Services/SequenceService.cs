using DbTest.GrpcService.Contexts;
using Grpc.Core;

namespace DbTest.GrpcService.Services;

public class SequenceService : Sequences.SequencesBase
{
    private readonly ILogger<SequenceService> _logger;
    private readonly DbTestContext _dbTestContext;
    public SequenceService(ILogger<SequenceService> logger, DbTestContext context)
    {
        _dbTestContext = context;
        _logger = logger;
    }

    public override Task<InsertResponse> InsertTVP(InsertCommand request, ServerCallContext context)
    {
        InsertResponse response;
        try
        {
            int count = _dbTestContext.InsertTVP(request.Sequences);
            response = new()
            {
                IsDone = true
            };
        }
        catch (Exception e)
        {
            response = new()
            {
                Message = $"{e.Message}",
                IsDone = false
            };
        }

        return Task.FromResult(response);
    }

    public override Task<SelectResponse> SelectTVP(SelectCommand request, ServerCallContext context)
    {
        SelectResponse selectResponse;
        try
        {
            selectResponse = new()
            {
                Sequences = { _dbTestContext.SelectTVP(request.Ids) },
                IsDone = true
            };
            selectResponse.IsDone = true;
        }
        catch (Exception e)
        {
            selectResponse = new()
            {
                Message = $"{e.Message}",
                IsDone = false
            };
        }

        return Task.FromResult(selectResponse);
    }
}