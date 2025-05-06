using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;
using Monopoly.Enums;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Autoloads;
using Monopoly.Scenes.Lands;
using Monopoly.Tools.BoardGenerator;
using FileAccess = Godot.FileAccess;

namespace Monopoly.Scenes.Managers;

public partial class BoardManager : Node {
    [Export]
    private NodePath? _boardPath;

    [Export]
    private Godot.Collections.Array<LandResource> _landResources = [];

    public override void _Ready() {
        GameSettings.RequestBaseGameSpecificPath("Lands", out var packPath);
        var builder = new BoardBuilder(packPath);

        foreach (var child in builder.BuildBoardContent()) {
            AddChild(child);
            _landResources.Add(child.Resource);
        }

        base._Ready();
    }

    public LandActionTags GetLandActionTag(int landId) =>
        _landResources.First(land => land.GetLandId == landId).GetTags;

    public LandResource GetLandResource(int landId) => _landResources.First(land => land.GetLandId == landId);

    public bool IsPropertyOwned(int landId) {
        var landResource = GetLandResource(landId);

        if (landResource is not PropertyResource propertyResource) {
            // Not a property
            throw new Exception($"Expected type: {typeof(PropertyResource)}.");
        }

        return propertyResource.OwnerId != -1;
    }

    public int GetPropertyOwnerId(int landId) {
        var landResource = GetLandResource(landId);

        if (landResource is not PropertyResource propertyResource) {
            throw new Exception($"Expected type: {typeof(PropertyResource)}.");
        }

        return propertyResource.OwnerId;
    }

    public int GetPropertyRent(int landId) {
        var landResource = GetLandResource(landId);

        if (landResource is not PropertyResource propertyResource) {
            // Not a property
            throw new Exception($"Expected type: {typeof(PropertyResource)}.");
        }

        var propertiesInGroup = _landResources.Where(resource => {
            if (resource is PropertyResource property) {
                return property.PropertyGroupId == propertyResource.PropertyGroupId;
            }

            return false;
        }).ToList();

        var playerHasColorSet = propertiesInGroup.All(resource => {
            if (resource is not PropertyResource property) return false;
            return property.OwnerId == propertyResource.OwnerId;
        });

        var propertyHasHouses = propertyResource.HouseCount > 0;

        var multiplier = (playerHasColorSet && !propertyHasHouses) ? 2 : 1;

        return propertyResource.GetRent() * multiplier;
    }

    public int GetPropertyBuyPrice(int landId) {
        var landResource = GetLandResource(landId);

        if (landResource is not PropertyResource propertyResource) {
            throw new Exception($"Expected type: {typeof(PropertyResource)}.");
        }

        return propertyResource.Price;
    }

    public List<LandBase> GetAllLands() {
        var lands = new List<LandBase>();

        foreach (var child in GetChildren()) {
            if (child is not LandBase land) {
                continue;
            }

            lands.Add(land);
        }

        return lands;
    }

    public void GivePlayerProperty(PlayerResource player, int landId) {
        if (GetLandResource(landId) is not PropertyResource land) return;
        
        player.Properties.Add(land);
        land.OwnerId = player.PlayerId;
    }
}