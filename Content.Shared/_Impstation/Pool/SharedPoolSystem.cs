using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Shared._Impstation.Pool;

public abstract partial class SharedPoolSystem : EntitySystem
{
    [Dependency] private readonly SharedSolutionContainerSystem _solutionContainer = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PoolComponent, SolutionContainerChangedEvent>(OnSolutionChanged);
    }

    public void OnSolutionChanged(Entity<PoolComponent> ent, ref SolutionContainerChangedEvent args)
    {
        if (args.Solution.Name != ent.Comp.SolutionName)
            return;

        if (args.Solution.Volume <= 0)
            return;

        UpdateAppearance((ent, ent.Comp));
    }

    private void UpdateAppearance(Entity<PoolComponent?, AppearanceComponent?> ent)
    {
        var (uid, pool, appearance) = ent;
        if (!Resolve(ent, ref pool, ref appearance))
            return;
        var alpha = FixedPoint2.Zero;

        var color = Color.White;
        if (_solutionContainer.ResolveSolution(uid,
                pool.SolutionName,
                ref pool.Solution,
                out var solution))
        {

            alpha = solution.Volume / solution.MaxVolume;

            color = solution.GetColor(_prototypeManager);
        }

        _appearance.SetData(ent, PoolVisuals.Alpha, alpha, appearance);
        _appearance.SetData(ent, PoolVisuals.SolutionColor, color, appearance);
    }

}
