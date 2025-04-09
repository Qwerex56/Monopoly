using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Monopoly.Enums;
using Monopoly.ResourcesModels;

namespace Monopoly.Scenes.Managers;

public partial class EventManager : Node {
    private GameManager _gameManager;
    private List<EventResource> _eventResources = [];

    private readonly List<int> _communityChestEventPool = [];
    private readonly List<int> _chanceCardEventPool = [];

    public override void _Ready() {
        if (GetParent() is GameManager gameManager) {
            _gameManager = gameManager;
        }
        
        base._Ready();
    }

    public void HandleEvent(PlayerResource player, EventLandResource eventLand) {
        var eventResource = eventLand.EventResource;

        foreach (var eventEffect in eventResource.EventEffects) {
            switch (eventEffect.Key) {
                case EventSpecialEffect.GiveMoneyToBank:
                    TakeMoneyFromPlayer(player, eventEffect.Value);
                    break;
                case EventSpecialEffect.TakeMoneyFromBank:
                    GiveMoneyToPlayer(player, eventEffect.Value);
                    break;
                case EventSpecialEffect.GiveMoneyToAllPlayers:
                    GiveMoneyToAllPlayers(player, eventEffect.Value);
                    break;
                case EventSpecialEffect.TakeMoneyFromAllPlayers:
                    TakeMoneyFromAllPlayers(player, eventEffect.Value);
                    break;
                case EventSpecialEffect.PayForHousesAndHotels:
                    break;
                case EventSpecialEffect.VisitPlace:
                    break;
                case EventSpecialEffect.DrawCommunityChest:
                    break;
                case EventSpecialEffect.DrawChance:
                    break;
                case EventSpecialEffect.GoToJail:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void GiveMoneyToPlayer(PlayerResource player, int amount) {
        var playerManager = _gameManager.GetPlayerManager;
        var playerId = player.PlayerId;
        
        playerManager.GiveMoneyToPlayer(playerId, amount);
    }

    private void TakeMoneyFromPlayer(PlayerResource player, int amount) {
        GD.Print("Action");
        var playerManager = _gameManager.GetPlayerManager;
        var playerId = player.PlayerId;
        
        playerManager.TakeMoneyFromPlayer(playerId, amount);
    }

    private void GiveMoneyToAllPlayers(PlayerResource player, int amount) {
        var playerManager = _gameManager.GetPlayerManager;
        var playerId = player.PlayerId;
        
        playerManager.GiveMoneyToAllPlayers(playerId, amount);
    }

    private void TakeMoneyFromAllPlayers(PlayerResource player, int amount) {
        var playerManager = _gameManager.GetPlayerManager;
        var playerId = player.PlayerId;
        
        playerManager.TakeMoneyFromAllPlayers(playerId, amount);
    }
    
    private int DrawCommunityChest() {
        if (_communityChestEventPool.Count <= 0) return -1;

        var communityChestCardId = _communityChestEventPool.First();
        _communityChestEventPool.RemoveAt(0);
        _communityChestEventPool.Add(communityChestCardId);
        return communityChestCardId;
    }

    private int DrawChanceCard() {
        if (_chanceCardEventPool.Count <= 0) return -1;
        
        var chanceCardId = _chanceCardEventPool.First();
        _chanceCardEventPool.RemoveAt(0);
        _chanceCardEventPool.Add(chanceCardId);
        return chanceCardId;
    }
}