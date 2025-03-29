using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Monopoly.ResourcesModels;

public partial class BoardResource : Resource {
    [Export] private int[] _tiles = [];
    
    public List<int> Tiles => _tiles.ToList();
}