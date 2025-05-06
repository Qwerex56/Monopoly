using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Monopoly.Enums;
using Monopoly.ResourcesModels;
using Monopoly.Scenes.Lands;

namespace Monopoly.Scenes.Managers;

public partial class GameManager : Node {
    [Export]
    private BoardManager _boardManager = null!;

    [Export]
    private PlayerManager _playerManager = null!;
    
    [Export]
    private EventManager _eventManager = null!;

    public override void _Ready() {
        ConnectLandsSignals();
        base._Ready();
    }

    private void HandlePlayerEnteredLand(in int playerId, in int landId) {
        GD.Print("Handle Player entered land.");
    }

    private void HandlePlayerVisitLand(in int playerId, in int landId) {
        GD.Print("Handle player stopped on land.");
        var landActionTags = _boardManager.GetLandActionTag(landId);

        if (landActionTags.HasFlag(LandActionTags.FreeLand)) {
            HandleFreeLand(playerId, landId);
        }

        if (landActionTags.HasFlag(LandActionTags.PropertyLand)) {
            HandlePropertyLand(playerId, landId);
        }

        if (landActionTags.HasFlag(LandActionTags.EventLand)) {
            HandleEventLand(playerId, landId);
        }
    }

    private void HandlePlayerExitedLand(in int playerId, in int landId) {
        GD.Print("Handle player exit land.");
    }

    #region LandActions

    private void HandleFreeLand(in int playerId, in int landId) {
    }

    private void HandleEventLand(in int playerId, in int landId) {
        GD.Print("Handle event land.");
        var landResource = _boardManager.GetLandResource(landId);
        var playerResource = _playerManager.GetPlayer(playerId);
        
        if (landResource is not EventLandResource eventLandResource) return;
        
        _eventManager.HandleEvent(playerResource, eventLandResource.EventResource);
    }

    private void HandlePropertyLand(in int playerId, in int landId) {
        var isPropertyOwned = _boardManager.IsPropertyOwned(landId);

        if (isPropertyOwned) {
            var payeeId = _boardManager.GetPropertyOwnerId(landId);
            var rentAmount = _boardManager.GetPropertyRent(landId);

            if (playerId == payeeId) return;
            
            _playerManager.PayMoneyToOtherPlayer(playerId, payeeId, rentAmount);
        }
        else {
            var buyAmount = _boardManager.GetPropertyBuyPrice(landId);
            var playerAccount = _playerManager.GetPlayerBankAccount(playerId);
            
            if (playerAccount < buyAmount) {
                GD.Print($"Not enough money to buy: {landId}. Player has {playerAccount}, needs at least: {buyAmount}");
                return;
            }
            
            _playerManager.TakeMoneyFromPlayer(playerId, buyAmount);

            var playerResource = _playerManager.GetPlayer(playerId);
            _boardManager.GivePlayerProperty(playerResource, landId);
        }
    }

    #endregion

    private void ConnectLandsSignals() {
        var landsNodes = _boardManager.GetAllLands();

        foreach (var land in landsNodes) {
            land.PlayerEntered += (playerId, landId) => HandlePlayerEnteredLand(playerId, landId);
            land.PlayerVisit += (playerId, landId) => HandlePlayerVisitLand(playerId, landId);
            land.PlayerExited += (playerId, landId) => HandlePlayerExitedLand(playerId, landId);
        }
        
    }

    public PlayerManager GetPlayerManager => _playerManager;
}