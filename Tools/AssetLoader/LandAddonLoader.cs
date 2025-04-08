using System;
using System.IO;
using Godot;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Lands;
using FileAccess = Godot.FileAccess;

namespace Monopoly.Tools.AssetLoader;

public class LandAddonLoader : IAddonLoader<LandBase, LandResource> {
    public LandBase Load(string resourcePath) {
        ArgumentException.ThrowIfNullOrEmpty(resourcePath);
        if (!IsPathValid(resourcePath))
            throw new FileNotFoundException($"Asset path: {nameof(resourcePath)} is not valid.");

        var resource = GD.Load<Resource>(resourcePath);

        if (resource is not LandResource propertyResource) {
            throw new TypeLoadException(
                $"Invalid resource type, got {resource.GetType()}, should be {typeof(LandResource)}.");
        }

        var landPropertyScene = GD.Load<PackedScene>("res://Scenes/Lands/land_base.tscn");

        var landProperty = landPropertyScene.Instantiate<LandBase>();
        landProperty.SetResource(propertyResource);

        return landProperty;
    }

    private static bool IsPathValid(string resourcePath) {
        return FileAccess.FileExists(resourcePath);
    }
}