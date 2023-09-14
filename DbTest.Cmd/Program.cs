using DbTest.Cmd;
using DbTest.Cmd.Models;

int part = 50_000;
Requests requests = new();

SequenceData data = new();
DateTime start = DateTime.Now;
List<string> sequences = data.GetData();
DateTime end = DateTime.Now;
Console.WriteLine($"Время генерации: {end - start}, items: {sequences.Count}");

start = DateTime.Now;
bool isDone = await requests.Insert(sequences, part);
end = DateTime.Now;
Console.WriteLine($"Время загрузки в Sql: {end - start}, отправка по {part}");

List<int> ids = Enumerable.Range(1, 1_000_000).ToList();
start = DateTime.Now;
List<SelectSequenceModel> models = await requests.Select(ids, part) ?? new();
end = DateTime.Now;
Console.WriteLine($"Время выгрузки с базы: {end - start}, items: {1_000_000}");
Console.ReadLine();