using DbTest.Cmd.Mapperly;
using DbTest.Cmd.Models;
using Grpc.Net.Client;

namespace DbTest.Cmd;

public class Requests
{
    private readonly Sequences.SequencesClient _client;
    public Requests()
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5062");
        _client = new(channel);
    }

    public async Task<bool> Insert(List<string> sequences, int part = 10_000)
    {
        bool isDone = true;
        int countFail = 10;
        while (sequences.Any())
        {
            List<string> source = sequences.Count > part
            ? sequences.GetRange(0, part)
            : sequences;
            
            if
            (
                (await _client.InsertTVPAsync
                (new InsertCommand
                    { Sequences = { source } }
                )).IsDone
            )
            {
                sequences.RemoveRange
                (
                    0,
                    source.Count
                );
            }
            else
            {
                if (countFail > 0) { countFail--; }
                else
                {
                    isDone = false;
                    break;
                }

                Console.WriteLine("Не удалось отправить, ожидаем 1 секунду");
                await Task.Delay(1000);
            }
        }
        return isDone;
    }

    public async Task<List<SelectSequenceModel>?> Select(List<int> ids, int part = 10_000)
    {
        SequenceMapperly mapperly = new();
        List<SelectSequenceModel> models = new(ids.Count);
        while (ids.Any())
        {
            List<int> source = ids.Count > part
            ? ids.GetRange(0, part)
            : ids;

            SelectResponse response = await _client.SelectTVPAsync(new SelectCommand { Ids = { source } });
            if (response.IsDone)
            {
                ids.RemoveRange(0, source.Count);
                models.AddRange(mapperly.ToModel(response.Sequences));
                continue;
            }

            Console.WriteLine(response.Message);
            return null;
        }

        return models;
    }
}