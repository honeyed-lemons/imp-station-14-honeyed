using Content.Server.Xenoarchaeology.XenoArtifacts.Triggers.Components;
using Content.Shared.Throwing;

namespace Content.Server.Xenoarchaeology.XenoArtifacts.Triggers.Systems;

public sealed class ArtifactLandSystem : EntitySystem
{
    [Dependency] private readonly ArtifactSystem _artifact = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ArtifactLandTriggerComponent, LandEvent>(OnLand);
    }

    private void OnLand(Entity< ArtifactLandTriggerComponent> ent, ref LandEvent args)
    {
        _artifact.TryActivateArtifact(ent, args.User);
    }
}