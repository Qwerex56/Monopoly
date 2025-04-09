using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Lands;

public partial class LandProperty : LandBase {
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
}