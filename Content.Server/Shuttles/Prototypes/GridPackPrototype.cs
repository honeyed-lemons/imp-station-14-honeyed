using Robust.Shared.Prototypes;

namespace Content.Server.Shuttles.Prototypes;

[Prototype]
public sealed partial class GridPackPrototype : IPrototype
{
    [ViewVariables]
    [IdDataField]
    public string ID { get; private set; } = default!;

    /// <summary>
    ///     List of grids.
    /// </summary>
    [DataField]
    public List<ProtoId<StaticGridPrototype>> Grids = new();
}
