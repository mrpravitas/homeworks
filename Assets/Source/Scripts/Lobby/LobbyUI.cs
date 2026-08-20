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

    private void OnEnable()
    {
        _lobbyManager.OnPlayersChanged += RefreshUI;
        _readyButton.onClick.AddListener(OnReadyClicked);
        _startButton.onClick.AddListener(OnStartGameClicked);
    }

    private void OnDisable()
    {
        _lobbyManager.OnPlayersChanged -= RefreshUI;
        _readyButton.onClick.RemoveListener(OnReadyClicked);
        _startButton.onClick.RemoveListener(OnStartGameClicked);
    }

    private void RefreshUI(IReadOnlyList<PlayerInfo> players)
    {
        RefreshPlayerList(players);
        RefreshButtons(players);
    }

    private void RefreshPlayerList(IReadOnlyList<PlayerInfo> players)
    {
        foreach (Transform child in _playerListContent)
            Destroy(child.gameObject);

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

    public void OnReadyClicked()
    {
        NetworkPlayer local = NetworkClient.localPlayer?.GetComponent<NetworkPlayer>();
        if (local != null)
            local.SetReady(!local.IsReady);
    }

    public void OnStartGameClicked()
    {
        _lobbyManager.StartGame();
    }
}
