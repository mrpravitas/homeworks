using Mirror;
using TMPro;
using UnityEngine;

public class GamePlayer : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _nicknameLabel;
    [SerializeField] private MeshRenderer _playerRenderer;

    [SyncVar]
    private string _nickname;
    [SyncVar]
    private Color _color;
    [SyncVar(hook = nameof(OnGameOverChanged))]
    private bool _isGameOver;

    public string Nickname => _nickname;

    public void SetData(string nickname, Color color)
    {
        _nickname = nickname;
        _color = color;
    }

    public override void OnStartClient()
    {
        ApplyData();
        Debug.Log($"GamePlayer spawned: nickname={_nickname}");
    }

    public override void OnStopClient()
    {
        Debug.Log($"LobbyPlayer disconnected: nickname={_nickname}");
    }

    public override void OnStartServer()
    {
        LeaderBoard.Instance?.RegisterPlayer(this);
        MatchTimer.OnGameFinished += OnGameFinished;
    }

    public override void OnStopServer()
    {
        MatchTimer.OnGameFinished -= OnGameFinished;
    }

    private void ApplyData()
    {
        _nicknameLabel.text = _nickname;
        _nicknameLabel.color = _color;

        _playerRenderer.material.color = _color;
    }

    [Server]
    private void OnGameFinished()
    {
        _isGameOver = true;
    }

    private void OnGameOverChanged(bool oldValue, bool newValue)
    {
        if (!_isGameOver)
        {
            return;
        }

        PlayerController controller = GetComponent<PlayerController>();
        controller.UnlockCursor();
        controller.enabled = false;
        
        GetComponent<Health>().enabled = false;
        GetComponent<GrenadeManager>().enabled = false;
        GetComponent<MedKitManager>().enabled = false;
        GetComponentInChildren<Weapon>().enabled = false;
    }
}
