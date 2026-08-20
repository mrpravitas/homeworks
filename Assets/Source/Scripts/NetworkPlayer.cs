using Mirror;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _nicknameLabel;
    [SerializeField] private MeshRenderer _playerRenderer;

    [SyncVar(hook = nameof(OnNicknameChanged))]
    private string _nickname;
    [SyncVar(hook = nameof(OnColorChanged))]
    private Color _color = Color.white;
    [SyncVar(hook = nameof(OnReadyChanged))]
    private bool _isReady;

    public bool IsReady => _isReady;

    public static event Action<uint, PlayerInfo> OnPlayerUpdated;
    public static event Action<uint> OnPlayerRemoved;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        OnPlayerRemoved?.Invoke(netId);
    }

    public override void OnStartServer()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        OnNicknameChanged("", _nickname);
        OnColorChanged(Color.white, _color);
        Broadcast();
    }

    public override void OnStartLocalPlayer()
    {
        CmdSetNickname("Player" + UnityEngine.Random.Range(1000, 9999));
        CmdSetColor(Color.white);
    }

    public void SetNickname(string nickname)
    {
        CmdSetNickname(nickname);
    }

    public void SetColor(Color color)
    {
        CmdSetColor(color);
    }

    public void SetReady(bool flag)
    {
        CmdSetReady(flag);
    }

    private void OnNicknameChanged(string oldNickname, string newNickname)
    {
        _nicknameLabel.text = newNickname;
        Broadcast();
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        _nicknameLabel.color = newColor;
        _playerRenderer.material.color = newColor;
        Broadcast();
    }

    private void OnReadyChanged(bool oldFlag, bool newFlag)
    {
        Broadcast();
    }

    private void Broadcast()
    {
        PlayerInfo info = new PlayerInfo
        {
            Nickname = _nickname,
            Color = _color,
            IsReady = _isReady,
        };
        OnPlayerUpdated?.Invoke(netId, info);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isServer) return;

        Transform start = NetworkManager.singleton.GetStartPosition();
        if (start != null)
        {
            transform.position = start.position;
            transform.rotation = start.rotation;
            RpcRelocate(start.position, start.rotation);
        }
    }

    [ClientRpc]
    private void RpcRelocate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    [Command]
    private void CmdSetNickname(string nickname)
    {
        _nickname = nickname;
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        _color = color;
    }

    [Command]
    private void CmdSetReady(bool flag)
    {
        _isReady = flag;
    }
}

public struct PlayerInfo
{
    public string Nickname;
    public Color Color;
    public bool IsReady;
}
