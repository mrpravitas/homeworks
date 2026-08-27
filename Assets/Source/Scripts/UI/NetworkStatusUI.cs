using Mirror;
using TMPro;
using UnityEngine;

public class NetworkStatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;

    private void OnEnable()
    {
        NetworkClient.OnConnectedEvent += UpdateStatus;
        NetworkClient.OnDisconnectedEvent += UpdateStatus;
    }

    private void OnDisable()
    {
        NetworkClient.OnConnectedEvent -= UpdateStatus;
        NetworkClient.OnDisconnectedEvent -= UpdateStatus;
    }

    private void Start()
    {
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        string role = NetworkServer.active ? "Host" : "Client";
        int players = NetworkManager.singleton.numPlayers;
        _statusText.text = NetworkServer.active ? 
            $"{role} | Players: {players}" : 
            role;
    }
}
