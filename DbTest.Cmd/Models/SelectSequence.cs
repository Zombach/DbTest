namespace DbTest.Cmd.Models;

public record SelectSequenceModel
{
    public int Id { get; init; }
    public string Sequence { get; init; }
}