using Content.Shared._Impstation.Pool;
using Content.Shared.FixedPoint;
using Robust.Client.GameObjects;

namespace Content.Client._Impstation.Pool;

public sealed class PoolSystem : SharedPoolSystem
{
    [Dependency] private readonly SpriteSystem _sprite = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PoolComponent, AppearanceChangeEvent>(OnPoolAppearance);
    }

    private void OnPoolAppearance(Entity<PoolComponent> ent, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        var baseColor = Color.White;

        var alpha = FixedPoint2.Zero;

        if (args.AppearanceData.TryGetValue(PoolVisuals.Alpha, out var alphaobj))
            alpha = (FixedPoint2)alphaobj;

        if (args.AppearanceData.TryGetValue(PoolVisuals.SolutionColor, out var colorObj))
        {
            var color = (Color)colorObj;
            color.WithAlpha(alpha.Float());
            _sprite.LayerSetColor(ent.Owner, 1, color);
        }
        else
        {
            baseColor.WithAlpha(alpha.Float());
            _sprite.LayerSetColor(ent.Owner, 1, args.Sprite.Color * baseColor);
        }
    }
}
