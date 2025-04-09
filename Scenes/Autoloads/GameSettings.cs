using System.Linq;
using Godot;

namespace Monopoly.Scenes.Autoloads;

public partial class GameSettings : Node {
    private const string BaseGamePath = "res://Addons/BaseGame/";
    
    public static GameSettings Instance { get; private set; } = null!;
    
    /// <summary>
    /// Path to selected addon pack. <br/>
    /// Note: Only supports absolute path
    /// </summary>
    private string _addonPackPath = string.Empty;
    
    public static string GetBaseGamePath => BaseGamePath;

    public override void _Ready() {
        Instance = this;
        base._Ready();
    }

    /// <summary>
    /// Tries to get addon pack path.
    /// </summary>
    /// <param name="addonPath">Param sent out with addon pack path or base game path.</param>
    /// <returns>True if addon pack path exists, false otherwise.</returns>
    public bool RequestAddonPackPath(out string addonPath) {
        if (!DirAccess.DirExistsAbsolute(_addonPackPath)) {
            addonPath = BaseGamePath;
            return true;
        }
        
        if (_addonPackPath == string.Empty) {
            addonPath = BaseGamePath;
            return false;
        }
        
        addonPath = _addonPackPath;
        return true;
    }

    /// <summary>
    /// Tries to get specific folder from addon pack.
    /// </summary>
    /// <param name="subDir"></param>
    /// <param name="packPath"></param>
    /// <returns></returns>
    public bool RequestAddonSpecificPath(string subDir, out string packPath) {
        if (RequestAddonPackPath(out var addonPath) is false) {
            packPath = BaseGamePath;
            return false;
        }
        
        var directories = DirAccess.Open(addonPath).GetDirectories();
        if (!directories.Contains(subDir)) {
            packPath = BaseGamePath;
            return false;
        }

        packPath = addonPath + $"/{subDir}/";
        return true;
    }

    public static bool RequestBaseGameSpecificPath(string subDir, out string packPath) {
        var directories = DirAccess.Open(BaseGamePath).GetDirectories();

        if (!directories.Contains(subDir)) {
            packPath = BaseGamePath;
            return false;
        }
        
        packPath = BaseGamePath + $"/{subDir}/";
        return true;
    }
}