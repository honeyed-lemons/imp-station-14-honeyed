using Content.Shared.Chemistry.Components;
using Content.Shared.FixedPoint;
using Robust.Shared.GameStates;

namespace Content.Shared._Impstation.Pool;
[RegisterComponent, NetworkedComponent]
public sealed partial class PoolComponent : Component
{
    [DataField("solution")]
    public string SolutionName = "pool";

    [DataField]
    public FixedPoint2 OverflowVolume = FixedPoint2.New(50);

    [ViewVariables]
    public Entity<SolutionComponent>? Solution = null;
}
