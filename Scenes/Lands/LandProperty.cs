using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Lands;

public partial class LandProperty : LandBase {
    [Signal]
    public delegate void PlayerStoppedOnOwnedPropertyEventHandler(int payer, int payee, int rentAmount);

    [Signal]
    public delegate void PlayerStoppedOnUnownedPropertyEventHandler(int playerId, int landId, int price);


    [Export]
    [ExportGroup("Property land")]
    private Label _priceTag = null!;

    [Export]
    [ExportGroup("Property land")]
    private Polygon2D _ribbon = null!;

    public override void _Ready() {
        base._Ready();

        if (Resource is not PropertyResource resource) return;

        _priceTag.Text = $"${resource.Price}";
        _ribbon.Color = resource.Color;
    }

    private void _on_scan_area_body_entered(Player.Player player) {
        GD.Print("Player entered");
        
        // if (player.PlayerResource.Position != Resource.Position) return;

        EmitSignalPlayerStopped(player.PlayerResource.PlayerId, Resource.GetLandId);

        if (Resource is not PropertyResource propertyResource) return;
        
        if (propertyResource.OwnerId == -1) {
            GD.Print("You can buy this property.");
            EmitSignalPlayerStoppedOnUnownedProperty(player.PlayerResource.PlayerId, propertyResource.GetLandId,
                propertyResource.Price);
        }
        else {
            EmitSignalPlayerStoppedOnOwnedProperty(player.PlayerResource.PlayerId, propertyResource.OwnerId,
                propertyResource.GetRent());
        }
    }

    private void _on_scan_area_body_exited(Player.Player player) {
        EmitSignalPlayerMovedBy(-1, Resource?.GetLandId ?? -1);
    }
}