using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Server.Shuttles.Prototypes;

[Prototype]
public sealed partial class StaticGridPrototype : IPrototype
{
    [ViewVariables]
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public ResPath? Path;

    /// <summary>
    /// When true, the grid is not visible via IFF
    /// </summary>
    [DataField]
    public bool Hide;
}
