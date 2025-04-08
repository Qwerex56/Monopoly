using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Lands;
using Monopoly.Tools.BoardGenerator;
using FileAccess = Godot.FileAccess;

namespace Monopoly.Scenes.Managers;

public partial class BoardManager : Node {
    private const string BasePath = "res://Addons/BaseGame/Lands";

    [Export]
    private NodePath? _boardPath;

    [Export]
    private PackedScene? _landScene;

    [Export]
    private Node? _gameSettings;

    // [Export]
    // private List<LandResource> _landResources = [];

    [Export]
    private Godot.Collections.Array<LandResource> _landResources = [];

    public override void _Ready() {
        var builder = new BoardBuilder();

        foreach (var child in builder.BuildBoardContent()) {
            AddChild(child);
            child.RequestReady();
        }

        base._Ready();
    }

    public void HandlePlayerBoughtProperty(int playerId, int landId) {
        var land = _landResources.First(resource => resource.GetLandId == landId);

        if (land is PropertyResource propertyResource) {
            propertyResource.OwnerId = playerId;
            GD.Print("Player bought property.");
        }
    }
}