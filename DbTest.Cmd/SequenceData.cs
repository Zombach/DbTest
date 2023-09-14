using SequenceGenerator.Library;

namespace DbTest.Cmd;

public class SequenceData
{
    public List<string> GetData()
    {
        Generation generation = new();
        List<string> sequences = generation.Start(new Source("XXXXXXXX", new Dictionary<int, IEnumerable<string>>
        {
            {0, new List<string>{ "Asp", "Ile", "Thr", "Lys", "Arg", "Ala", "Glu", "Ser", "Tyr"}},
            {1, new List<string>{ "Asp", "Ile", "Thr", "Lys", "Arg", "Ala"}},
            {2, new List<string>{ "Asp", "Ile", "Thr", "Lys", "Arg", "Ala", "Glu", "Tyr" }},
            {3, new List<string>{ "Tyr", "Ala", "Val", "Lys", "His", "Leu", "Met", "Thr", "Arg", "Gln", "Cys", "Ser", "Asn" }},
            {4, new List<string>{ "Asp", "Ile", "Thr", "Lys", "Arg", "Ala", "Glu", "Ser", "Tyr" }},
            {5, new List<string>{ "Arg", "Gly", "Leu", "Lys", "Ser", "Asn" }},
            {6, new List<string>{ "Arg", "Gly", "Leu", "Lys", "Thr", "Gln", "Ser", "Asn" }},
            {7, new List<string>{ "Tyr", "Ala", "Val", "Lys", "His", "Leu", "Met", "Thr", "Arg", "Cys", "Ser", "Asn" }},
        }));
        return sequences;
    }
}