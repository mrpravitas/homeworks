using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoard : NetworkBehaviour
{
    private readonly Dictionary<uint, PlayerScoreEntry> _entries = new();
    private readonly SyncList<PlayerScoreEntry> _synced = new();

    public static LeaderBoard Instance { get; private set; }

    public SyncList<PlayerScoreEntry> Leaderboard => _synced;

    public void Awake()
    {
        Instance = this;
    }

    [Server]
    public void RegisterPlayer(GamePlayer player)
    {
        _entries[player.netId] = new PlayerScoreEntry
        {
            NetId = player.netId,
            Nickname = player.Nickname,
            Kills = 0,
            Deaths = 0,
            Score = 0
        };
        SortEntries();
    }

    private void OnEnable()
    {
        Health.OnPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        Health.OnPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied(uint victimNetId, uint killerNetId)
    {
        if (_entries.TryGetValue(victimNetId, out var victim))
        {
            victim.Deaths++;
            victim.Score = CalculateScore(victim);
            _entries[victimNetId] = victim;
        }

        if (killerNetId != 0 && killerNetId != victimNetId && _entries.TryGetValue(killerNetId, out var killer))
        {
            killer.Kills++;
            killer.Score = CalculateScore(killer); 
            _entries[killerNetId] = killer;
        }

        SortEntries();
    }

    private int CalculateScore(PlayerScoreEntry entry)
    {
        return Mathf.Max(0, entry.Kills * 3 - entry.Deaths);
    }

    private void SortEntries()
    {
        var order = _entries.Values
            .OrderByDescending(e => e.Score)
            .ThenBy(e => e.Deaths)
            .ThenBy(e => e.Nickname);

        _synced.Clear();
        _synced.AddRange(order);
    }
}
