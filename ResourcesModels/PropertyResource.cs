using Godot;
using Godot.Collections;
using CollectionExtensions = System.Collections.Generic.CollectionExtensions;

namespace Monopoly.ResourcesModels;

public partial class PropertyResource : LandResource {
    [Export] private int _price;
    [Export] private int _houseCount;
    [Export] private Dictionary<int, int> _rent = new();
    [Export] private int _ownerId = -1; // -1 when no one owns it
    [Export] private int _propertyGroupId = -1;
    
    public int Price => _price;
    public int HouseCount => _houseCount;
    public Dictionary<int, int> Rent => _rent;
    public int OwnerId => _ownerId;
    public int PropertyGroupId => _propertyGroupId;

    public int GetRent() => CollectionExtensions.GetValueOrDefault(_rent, _houseCount, -1);
}