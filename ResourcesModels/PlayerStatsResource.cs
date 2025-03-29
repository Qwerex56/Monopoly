using Godot;

namespace Monopoly.ResourcesModels;

/// <summary>
/// Used to initialize player starting stats, this resource is not meant to change.
/// </summary>
[Tool]
public partial class PlayerStatsResource : Resource {
    [Export] private int _startingMoney = 0;
    [Export] private Color _counterColor = Colors.WhiteSmoke;
    
    public int StartingMoney => _startingMoney;
    public Color CounterColor => _counterColor;
}