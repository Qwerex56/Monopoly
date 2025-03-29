using Godot;
using Monopoly.Enums;

namespace Monopoly.ResourcesModels;

public partial class EventResource : Resource {
    [Export] private EventCardTypeEnum _eventCardTypeEnum;
    [Export] private string _name = string.Empty;
    [Export] private string _description = string.Empty;
    [Export] private int _moneyChange;
    [Export] private int _specialEffect;
}