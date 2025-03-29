using Godot;

namespace Monopoly.ResourcesModels;

public class GameSettingsResource {
    [Export] private int _passingGoReward = 200;
    [Export] private int _housesInPool = 30;
    [Export] private int _hotelsInPool = 15;
    
    public int PassingGoReward => _passingGoReward;
    public int HousesInPool => _housesInPool;
    public int HotelsInPool => _hotelsInPool;
}