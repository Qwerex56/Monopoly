using System;
using System.Collections.Generic;
using Godot;

namespace Monopoly.ResourcesModels;

/// <summary>
/// Used to initialize player starting stats, this resource is not meant to change.
/// </summary>
public partial class PlayerResource : Resource {
    [Export]
    private int _playerId;

    [Export]
    private int _money;

    [Export]
    private Color _counterColor = Colors.WhiteSmoke;

    private int _position = 4;

    private readonly List<PropertyResource> _properties = [];
    private readonly List<PropertyResource> _mortgagedProperties = [];

    public int PlayerId => _playerId;

    public int Money {
        get => _money;
        set => _money = value;
    }

    public Color CounterColor => _counterColor;

    public int Position {
        get => _position;
        set => _position = value;
    }
    
    public List<PropertyResource> Properties => _properties;
}