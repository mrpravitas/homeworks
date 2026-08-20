using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private LobbyManager _lobbyManager;
    [SerializeField] private Transform _playerListContent;
    [SerializeField] private Button _readyButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _readyButtonText;
    [SerializeField] private TMP_InputField _nicknameInput;
    [SerializeField] private Button _applyButton;
    [SerializeField] private Image _colorPreview;
    [SerializeField] private Button[] _colorButtons;
    [SerializeField] private Color[] _paletteColors;

    private Color _selectedColor = Color.white;

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

        foreach (Transform child in _playerListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerInfo info in players)
        {
            GameObject go = new GameObject("Slot");
            go.transform.SetParent(_playerListContent, false);

            TMP_Text text = go.AddComponent<TextMeshProUGUI>();
            string ready = info.IsReady ? " [Ready]" : "";
            text.text = $"{info.Nickname}{ready}";
            text.color = info.Color;
            text.fontSize = 24;
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
            NetworkPlayer local = NetworkClient.localPlayer.GetComponent<NetworkPlayer>();
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
        NetworkPlayer local = NetworkClient.localPlayer?.GetComponent<NetworkPlayer>();
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
        NetworkPlayer local = NetworkClient.localPlayer?.GetComponent<NetworkPlayer>();
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
