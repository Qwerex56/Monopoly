using System.Threading.Tasks;
using Godot;

namespace Monopoly.Scenes.Managers;

public partial class GameManager : Node {
    [Export]
    private BoardManager _boardManager = null!;

    [Export]
    private PlayerManager _playerManager = null!;

    private void HandlePlayerStoppedOnOwnedLand(int payerId, int payeeId, int rentAmount) {
        _playerManager.HandleRentDue(payerId, payeeId, rentAmount);
    }

    // ReSharper disable once AsyncVoidMethod
    private async void HandlePlayerStoppedOnUnownedLand(int playerId, int landId, int price) {
        if (await _playerManager.HandlePlayerPurchaseProperty(playerId, price)) {
            _boardManager.HandlePlayerBoughtProperty(playerId, landId);
        }
        else {
            GD.PrintErr($"Player {playerId} did not purchase.");
        }
    }
}