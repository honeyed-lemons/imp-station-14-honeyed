using Content.Shared.Chemistry.Components;
using Content.Shared.FixedPoint;

namespace Content.Server._Impstation.Pool;
[RegisterComponent]
public sealed partial class PoolComponent : Component
{
    [DataField("solution")]
    public string SolutionName = "pool";

    [DataField]
    public FixedPoint2 OverflowVolume = FixedPoint2.New(50);

    [ViewVariables]
    public Entity<SolutionComponent>? Solution = null;

}
