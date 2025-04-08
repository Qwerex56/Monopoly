using System;
using Godot;
using Monopoly.Enums;

namespace Monopoly.ResourcesModels;

public partial class LandResource : Resource {
    [Export] private int _position; // Also an Id
    [Export] private string _name = string.Empty;
    
    [Export] private LandSizeEnum _size;
    [Export] private Texture2D? _icon;
    [Export] private Color _color;
    
    public int Position => _position;
    public string Name => _name;
    
    public LandSizeEnum Size => _size;
    public Texture2D? Icon => _icon;
    public Color Color => _color;

    public int GetLandId => _position;
}