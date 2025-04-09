using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Lands;

public partial class LandBase : Polygon2D {
    [Signal]
    public delegate void PlayerVisitEventHandler(int playerId, int landId);

    [Signal]
    public delegate void PlayerEnteredEventHandler(int playerId, int landId);
    
    [Signal]
    public delegate void PlayerExitedEventHandler(int playerId, int landId);
    

    [Export]
    [ExportGroup("Land base")]
    private Sprite2D? _icon;

    [Export]
    [ExportGroup("Land base")]
    private Label? _name;

    [Export]
    private LandResource _resource = null!;

    public LandResource Resource {
        get => _resource;
        private set => SetResource(value);
    }

    public int GetLandId => _resource.GetLandId;

    public override void _Ready() {
        if (_icon != null) _icon.Texture = _resource.Icon;
        if (_name != null) _name.Text = _resource.Name;

        base._Ready();
    }

    /// <summary>
    /// Sets new resource to LandBase.
    /// Does NOT update the current state of node, it should be managed manually by calling RequestReady
    /// </summary>
    /// <param name="resource"></param>
    public virtual void SetResource(LandResource resource) {
        _resource = resource;
    }

    private void OnScanAreaBodyEntered(Player.Player player) {
        var playerDestinationPosition = player.PlayerResource.Position;
        
        GD.Print($"Player: {player.GetPlayerId} entered land {GetLandId}");
        EmitSignalPlayerEntered(player.GetPlayerId, GetLandId);
        
        if (_resource.Position != playerDestinationPosition) {
            return;
        }

        GD.Print($"Player: {player.GetPlayerId} on land: {GetLandId}.");
        EmitSignalPlayerVisit(player.GetPlayerId, GetLandId);
    }

    private void OnScanAreaBodyExited(Player.Player player) {
        GD.Print($"Player: {player.GetPlayerId} exited land: {GetLandId}.");
        EmitSignalPlayerExited(player.GetPlayerId, GetLandId);
    }
}