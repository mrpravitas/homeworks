using Mirror;
using UnityEngine;

public class LeaderBoardUI : NetworkBehaviour
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _rowParent;
    [SerializeField] private GameObject _rowPrefab;

    [SyncVar(hook = nameof(OnGameOverChanged))]
    private bool _isGameOver;

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
        Rebuild();
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
        Rebuild();
    }

    private void Rebuild()
    {
        foreach (Transform child in _rowParent)
        {
            Destroy(child.gameObject);
        }

        GameObject header = Instantiate(_rowPrefab, _rowParent);
        header.GetComponent<LeaderBoardRow>().SetHeader();

        var rows = _leaderBoard.Leaderboard;
        for (int i = 0; i < rows.Count; i++)
        {
            GameObject row = Instantiate(_rowPrefab, _rowParent);
            row.GetComponent<LeaderBoardRow>().Set(rows[i], i == 0 && _isGameOver);
        }
    }

    private void OnGameOverChanged(bool oldValue, bool newValue)
    {
        _panel.SetActive(true);
        Rebuild();
    }
}
