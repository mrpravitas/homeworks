using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private LobbyManager _lobbyManager;
    [SerializeField] private Transform _playerListContent;
    [SerializeField] private GameObject _playerRowPrefab;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _readyButtonText;
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Image _colorPreview;
    [SerializeField] private Button[] _colorButtons;
    [SerializeField] private Color[] _paletteColors;

    private Color _selectedColor = Color.white;

    private readonly List<LobbyRow> _rows = new();

    private void OnEnable()
    {
        _lobbyManager.OnPlayersChanged += RefreshUI;
        _readyButton.onClick.AddListener(OnReadyClicked);
        _startButton.onClick.AddListener(OnStartGameClicked);
        _applyButton.onClick.AddListener(OnApplyClicked);

        for (int i = 0; i < _colorButtons.Length; i++)
        {
            int index = i;
            _colorButtons[i].onClick.AddListener(() => OnColorClicked(index));
        }
    }

    private void OnDisable()
    {
        _lobbyManager.OnPlayersChanged -= RefreshUI;
        _readyButton.onClick.RemoveListener(OnReadyClicked);
        _startButton.onClick.RemoveListener(OnStartGameClicked);
        _applyButton.onClick.RemoveListener(OnApplyClicked);

        foreach (var button in _colorButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void RefreshUI(IReadOnlyList<PlayerInfo> players)
    {
        RefreshPlayerList(players);
        RefreshButtons(players);
    }

    private void RefreshPlayerList(IReadOnlyList<PlayerInfo> players)
    {
        if (_playerListContent == null)
        {
            return;
        }

        while (_rows.Count < players.Count)
        {
            _rows.Add(Instantiate(_playerRowPrefab, _playerListContent).GetComponent<LobbyRow>());
        }

        for (int i = 0; i < _rows.Count; i++)
        {
            bool isVisible = i < players.Count;
            _rows[i].gameObject.SetActive(isVisible);
            if (isVisible)
            {
                _rows[i].Set(players[i]);
            }
        }
    }

    private void RefreshButtons(IReadOnlyList<PlayerInfo> players)
    {
        if (_readyButton == null)
        {
            return;
        }

        bool isHost = NetworkServer.active && NetworkClient.activeHost;
        bool hasLocalPlayer = NetworkClient.localPlayer != null;

        _readyButton.gameObject.SetActive(hasLocalPlayer);
        _startButton.gameObject.SetActive(isHost);

        if (hasLocalPlayer)
        {
            LobbyPlayer local = NetworkClient.localPlayer.GetComponent<LobbyPlayer>();
            _readyButtonText.text = local.IsReady ? "Unready" : "Ready";
        }

        _startButton.interactable = isHost && _lobbyManager.CanStartGame();
    }

    private void OnColorClicked(int index)
    {
        if (index < _paletteColors.Length)
        {
            _selectedColor = _paletteColors[index];
            _selectedColor.a = 1f;
            _colorPreview.color = _selectedColor;
        }
    }

    private void OnApplyClicked()
    {
        LobbyPlayer local = NetworkClient.localPlayer?.GetComponent<LobbyPlayer>();
        if (local == null)
        {
            return;
        }

        string nickname = _nicknameInput.text;
        if (!string.IsNullOrWhiteSpace(nickname))
        {
            local.SetNickname(nickname);
        }

        local.SetColor(_selectedColor);
    }

    public void OnReadyClicked()
    {
        LobbyPlayer local = NetworkClient.localPlayer?.GetComponent<LobbyPlayer>();
        if (local != null)
        {
            local.SetReady(!local.IsReady);
        }
    }

    public void OnStartGameClicked()
    {
        _lobbyManager.StartGame();
    }
}
