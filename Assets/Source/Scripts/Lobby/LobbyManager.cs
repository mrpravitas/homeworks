using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private string _gameScene; 
    [SerializeField] private int _minPlayers;

    private Dictionary<uint, PlayerInfo> _players = new();

    public event Action<IReadOnlyList<PlayerInfo>> OnPlayersChanged;

    private void OnEnable()
    {
        NetworkPlayer.OnPlayerUpdated += HandlePlayerUpdated;
        NetworkPlayer.OnPlayerRemoved += HandlePlayerRemoved;
    }

    private void OnDisable()
    {
        NetworkPlayer.OnPlayerUpdated -= HandlePlayerUpdated;
        NetworkPlayer.OnPlayerRemoved -= HandlePlayerRemoved;
    }

    private void HandlePlayerUpdated(uint id, PlayerInfo data)
    {
        _players[id] = data;
        NotifyUI();
    }

    private void HandlePlayerRemoved(uint id)
    {
        _players.Remove(id);
        NotifyUI();
    }

    private void NotifyUI()
    {
        OnPlayersChanged?.Invoke(_players.Values.ToList());
    }

    public bool CanStartGame()
    {
        return NetworkServer.active &&
            _players.Count >= _minPlayers &&
            _players.Values.All(p => p.IsReady);
    }

    public void StartGame()
    {
        if (CanStartGame())
        {
            NetworkManager.singleton.ServerChangeScene(_gameScene);
        }
    }
}
