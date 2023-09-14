using DbTest.Cmd.Models;
using Google.Protobuf.Collections;
using Riok.Mapperly.Abstractions;

namespace DbTest.Cmd.Mapperly;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public partial class SequenceMapperly
{
    public partial List<SelectSequenceModel> ToModel(RepeatedField<SelectSequence> selectResponse);

    [MapProperty(nameof(SelectSequence.Seq), nameof(SelectSequenceModel.Sequence))]
    private partial SelectSequenceModel ToModel(SelectSequence selectResponse);
}