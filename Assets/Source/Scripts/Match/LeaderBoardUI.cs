using Mirror;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField] private LeaderBoard _leaderBoard;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _rowParent;
    [SerializeField] private GameObject _rowPrefab;

    private void OnEnable()
    {
        if (_leaderBoard != null)
        {
            _leaderBoard.Leaderboard.Callback += OnLeaderboardChanged;
        }
    }

    private void OnDisable()
    {
        if (_leaderBoard != null)
        {
            _leaderBoard.Leaderboard.Callback -= OnLeaderboardChanged;
        }
    }

    private void Start()
    {
        Rebuild();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _panel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _panel.SetActive(false);
        }
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
            row.GetComponent<LeaderBoardRow>().Set(rows[i]);
        }
    }
}
