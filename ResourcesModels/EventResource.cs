using System;
using Godot;
using Godot.Collections;
using Monopoly.Enums;

namespace Monopoly.ResourcesModels;

public partial class EventResource : Resource {
    [Export]
    private int _eventId;

    [Export]
    private string _name = string.Empty;

    [Export]
    private string _description = string.Empty;

    [Export]
    private EventCardPoolEnum _eventCardPoolEnum;

    [Export]
    private Dictionary<EventSpecialEffect, int> _eventEffects = [];
    
    public int EventId => _eventId;
    public string Name => _name;
    public string Description => _description;
    public EventCardPoolEnum EventCardPoolEnum => _eventCardPoolEnum;
    public Dictionary<EventSpecialEffect, int> EventEffects => _eventEffects;
}