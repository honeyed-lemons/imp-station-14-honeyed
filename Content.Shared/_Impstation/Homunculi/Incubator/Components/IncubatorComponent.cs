using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._Impstation.Homunculi.Incubator.Components;

[RegisterComponent, AutoGenerateComponentPause, AutoGenerateComponentState]
public sealed partial class IncubatorComponent : Component
{
    /// <summary>
    /// How much charge a single use expends.
    /// </summary>
    [DataField]
    public float ChargeUse = 200f;

    [DataField]
    public bool Incubating;

    /// <summary>
    ///     How long to wait before finishing incubation.
    /// </summary>
    [DataField, AutoNetworkedField]
    public TimeSpan IncubationDuration = TimeSpan.FromMinutes(1);

    /// <summary>
    ///     When incubation is finished.
    /// </summary>
    [DataField, AutoPausedField, Access(typeof(SharedIncubatorSystem))]
    public TimeSpan? IncubationFinishTime;


    public EntityUid? PlayingStream;

    [DataField]
    public SoundSpecifier?  LoopingSound = new SoundPathSpecifier("/Audio/Machines/spinning.ogg");

    [DataField]
    public string BeakerSlotId = "beaker_slot";

}
