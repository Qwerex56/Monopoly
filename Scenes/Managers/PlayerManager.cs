using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Managers;

public partial class PlayerManager : Node {
    [Export]
    private Godot.Collections.Array<PlayerResource> _players = [];

    public void HandleRentDue(int payerId, int payeeId, int rentAmount) {
        // TODO: Add some checks if payer will become a bankrupt

        var payer = _players.First(player => player.PlayerId == payerId);
        var payee = _players.First(player => player.PlayerId == payeeId);

        if (payer is null || payee is null) return;

        payer.Money -= rentAmount;
        payee.Money += rentAmount;

        GD.Print($"Player {payer.PlayerId} has stopped on {payee.PlayerId} and need to pay him {rentAmount}$." +
                 $" Payer has now {payer.Money}$ and payee has now {payee.Money}$.");
    }

    public async Task<bool> HandlePlayerPurchaseProperty(int playerId, int price) {
        // TODO: This await is for simulation purposes, there should be call to UI
        GD.Print("Player is thinking.");
        await Task.Delay(2000);
        GD.Print("Player decided to buy.");
        
        var player = _players.First(player => player.PlayerId == playerId);
        player.Money -= price;

        return true;
    }
}