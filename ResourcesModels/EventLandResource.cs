using Godot;
using Monopoly.Enums;

namespace Monopoly.ResourcesModels;

public partial class EventLandResource : LandResource {
    [Export]
    private EventResource _eventResource;
    
    public EventResource EventResource => _eventResource;
}