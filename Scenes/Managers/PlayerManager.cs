using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Managers;

public partial class PlayerManager : Node {
    private List<PlayerResource> _players = [];

    public List<PlayerResource> Players => _players;

    public PlayerResource GetPlayer(int playerId) {
        return _players.First(p => p.PlayerId == playerId);
    }

    public override void _Ready() {
        foreach (var child in GetChildren()) {
            if (child is Player.Player player) {
                _players.Add(player.PlayerResource);
            }
        }

        base._Ready();
    }

    public void PayMoneyToOtherPlayer(int payerId, int payeeId, int amount) {
        var payer = GetPlayer(payerId);
        var payee = GetPlayer(payeeId);
    }

    public void BorrowMoneyFromOtherPlayer(int payerId, int payeeId, int amount) {
        PayMoneyToOtherPlayer(payeeId, payerId, amount);
    }

    public void GiveMoneyToAllPlayers(int payerId, int amount) {
        foreach (var payee in _players.Where(payee => payee.PlayerId != payerId)) {
            PayMoneyToOtherPlayer(payerId, payee.PlayerId, amount);
        }
    }

    public void TakeMoneyFromAllPlayers(int playerId, int amount) {
    }

    public void TakeMoneyFromPlayer(int payerId, int amount) {
        GD.Print($"Player: {payerId} pays {amount}$.");
        var player = GetPlayer(payerId);
        player.Money -= amount;
        GD.Print(player.Money);
    }

    public void GiveMoneyToPlayer(int payeeId, int amount) {
        var player = GetPlayer(payeeId);
        player.Money += amount;
    }
}