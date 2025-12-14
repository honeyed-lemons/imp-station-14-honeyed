using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Content.Server.Spreader;
using Content.Shared.Atmos;
using Content.Shared.Chemistry.Components;
using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.FixedPoint;
using Robust.Shared.Collections;
using Robust.Shared.Map.Components;
using Robust.Shared.Prototypes;

namespace Content.Server._Impstation.Pool;

public sealed class PoolSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly SharedSolutionContainerSystem _solutionContainer = default!;

    private EntityQuery<PoolComponent> _poolQuery;

    public override void Initialize()
    {
        _poolQuery = GetEntityQuery<PoolComponent>();

        SubscribeLocalEvent<PoolComponent, SolutionChangedEvent>(OnSolutionChanged);
        SubscribeLocalEvent<PoolComponent, SpreadNeighborsEvent>(OnPoolSpread);

    }

    private void OnPoolSpread(Entity<PoolComponent> ent, ref SpreadNeighborsEvent args)
    {
        if (!_solutionContainer.ResolveSolution(ent.Owner, ent.Comp.SolutionName, ref ent.Comp.Solution))
            return;

        var resolvedNeighbors = new ValueList<(Solution solution, Entity<PoolComponent> entity)>();

        if (args.Neighbors.Count <= 0)
            return;

        foreach (var neighbor in args.Neighbors)
        {
            if (!_poolQuery.TryGetComponent(neighbor, out var poolComp))
                continue;

            if (!_solutionContainer.ResolveSolution(neighbor, poolComp.SolutionName, ref poolComp.Solution))
                continue;

            resolvedNeighbors.Add((poolComp.Solution.Value.Comp.Solution,(neighbor,poolComp)));
        }

        resolvedNeighbors.Sort(
            (x, y) =>
                x.solution.Volume.CompareTo(y.solution.Volume));

        foreach (var neighbor in resolvedNeighbors)
        {
            if (neighbor.solution.Volume > ent.Comp.Solution.Value.Comp.Solution.Volume)
                continue;

            var average = (neighbor.solution.Volume + ent.Comp.Solution.Value.Comp.Solution.Volume) / 2;

            var difference = ent.Comp.Solution.Value.Comp.Solution.Volume - average;

            if (difference <= FixedPoint2.Zero)
                continue;

            var split = ent.Comp.Solution.Value.Comp.Solution.SplitSolution(difference);

            neighbor.solution.AddSolution(split, _prototypeManager);

            EnsureComp<ActiveEdgeSpreaderComponent>(neighbor.entity);
            args.Updates--;

            if (args.Updates <= 0)
                break;
        }

    }

    public void OnSolutionChanged(Entity<PoolComponent> ent, ref SolutionChangedEvent args)
    {

    }
}

