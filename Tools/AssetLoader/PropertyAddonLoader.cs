using System;
using System.IO;
using Godot;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Lands;
using FileAccess = Godot.FileAccess;

namespace Monopoly.Tools.AssetLoader;

/// <summary>
/// Property asset loader, this class loads property resources and builds game object using
/// loaded asset.
/// If objects does not update themselves after adding to the node tree, the RequestReady should be called
/// </summary>
/// <exception cref="ArgumentException">Thrown if resourceProperty is null or empty.</exception>
/// <exception cref="FileNotFoundException">Thrown if file does not exist.</exception>
public class PropertyAddonLoader : IAddonLoader<LandProperty, PropertyResource> {
    public LandProperty Load(string resourcePath) {
        ArgumentException.ThrowIfNullOrEmpty(resourcePath);
        if (!IsPathValid(resourcePath))
            throw new FileNotFoundException($"Asset path: {nameof(resourcePath)} is not valid.");

        var resource = GD.Load<Resource>(resourcePath);

        if (resource is not PropertyResource propertyResource) {
            throw new TypeLoadException(
                $"Invalid resource type, got {resource.GetType()}, should be {typeof(PropertyResource)}.");
        }

        var landPropertyScene = GD.Load<PackedScene>("res://Scenes/Lands/land_property.tscn");

        var landProperty = landPropertyScene.Instantiate<LandProperty>();
        landProperty.SetResource(propertyResource);

        return landProperty;
    }

    private static bool IsPathValid(string resourcePath) {
        return FileAccess.FileExists(resourcePath);
    }
}