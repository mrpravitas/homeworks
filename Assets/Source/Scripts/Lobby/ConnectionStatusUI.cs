using Mirror;
using TMPro;
using UnityEngine;

public class ConnectionStatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;

    private void Start()
    {
        if (NetworkManager.singleton.mode != NetworkManagerMode.Offline)
        {
            SetStatus("Connected");
        }
        else
        {
            SetStatus("Disconnected");
        }
    }

    private void OnEnable()
    {
        NetworkClient.OnConnectedEvent += OnConnected;
        NetworkClient.OnDisconnectedEvent += OnDisconnected;
        NetworkClient.OnErrorEvent += OnError;
    }

    private void OnDisable()
    {
        NetworkClient.OnConnectedEvent -= OnConnected;
        NetworkClient.OnDisconnectedEvent -= OnDisconnected;
        NetworkClient.OnErrorEvent -= OnError;
    }

    private void OnConnected()
    {
        SetStatus("Connected");
    }

    private void OnDisconnected()
    {
        SetStatus("Disconnected");
    }

    private void OnError(TransportError error, string reason)
    {
        SetStatus($"Error: {error} - {reason}");
    }

    private void SetStatus(string status)
    {
        _statusText.text = status;
    }
}
