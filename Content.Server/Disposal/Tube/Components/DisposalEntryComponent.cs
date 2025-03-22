using Content.Server.Disposal.Unit.EntitySystems;

namespace Content.Server.Disposal.Tube.Components
{
    [RegisterComponent]
    [Access(typeof(DisposalTubeSystem), typeof(DisposalUnitSystem))]
    public sealed partial class DisposalEntryComponent : Component
    {
        [DataField]
        public string HolderPrototypeId = "DisposalHolder"; //imp edit
    }
}
