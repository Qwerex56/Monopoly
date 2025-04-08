using System.IO;
using Godot;

namespace Monopoly.Tools.AssetLoader;

/// <summary>
/// Interface to load single resource file used in game.
/// </summary>
/// <typeparam name="TNode">Type of object.</typeparam>
/// <typeparam name="TResource">Type of asset that values will be loaded from.</typeparam>
public interface IAddonLoader<out TNode, TResource> where TNode : Node where TResource : Resource {
    public TNode Load(string resourcePath);

    protected static bool IsPathValid(string resourcePath) {
        return resourcePath.EndsWith(".tres") && Path.Exists(resourcePath);
    }
}