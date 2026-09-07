using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardUI : NetworkBehaviour
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _rowParent;
    [SerializeField] private GameObject _rowPrefab;

    [SyncVar(hook = nameof(OnGameOverChanged))]
    private bool _isGameOver;

    private readonly List<LeaderBoardRow> _rows = new();
    private LeaderBoardRow _headerRow;

    private void OnEnable()
    {
        _leaderBoard.Leaderboard.Callback += OnLeaderboardChanged;
        MatchTimer.OnGameFinished += OnGameFinished;
    }

    private void OnDisable()
    {
        _leaderBoard.Leaderboard.Callback -= OnLeaderboardChanged;
        MatchTimer.OnGameFinished -= OnGameFinished;
    }

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _panel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _panel.SetActive(false);
        }
    }

    [Server]
    private void OnGameFinished()
    {
        _isGameOver = true;
    }

    private void OnLeaderboardChanged(SyncList<PlayerScoreEntry>.Operation op, int index,
                                      PlayerScoreEntry oldItem, PlayerScoreEntry newItem)
    {
        Refresh();
    }

    private void Refresh()
    {
        if (_headerRow == null)
        {
            _headerRow = Instantiate(_rowPrefab, _rowParent).GetComponent<LeaderBoardRow>();
            _headerRow.SetHeader();
        }

        var rows = _leaderBoard.Leaderboard;

        while (_rows.Count < rows.Count)
        {
            _rows.Add(Instantiate(_rowPrefab, _rowParent).GetComponent<LeaderBoardRow>());
        }

        for (int i = 0; i < _rows.Count; i++)
        {
            bool isVisible = i < rows.Count;
            _rows[i].gameObject.SetActive(isVisible);
            if (isVisible)
            {
                _rows[i].Set(rows[i], i == 0 && _isGameOver);
            }
        }
    }

    private void OnGameOverChanged(bool oldValue, bool newValue)
    {
        _panel.SetActive(true);
        Refresh();
    }
}
