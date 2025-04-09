using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Player;

public partial class Player : CharacterBody2D {
    [Export(hintString: "Hello")]
    private PlayerResource _playerResource = null!;

    public PlayerResource PlayerResource {
        get => _playerResource;
        set => _playerResource = value;
    }
    
    public int GetPlayerId => PlayerResource.PlayerId;    
    
}