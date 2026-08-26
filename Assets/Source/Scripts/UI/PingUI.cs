using Mirror;
using TMPro;
using UnityEngine;

public class PingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pingText;

    private void Update()
    {
        double rtt = NetworkTime.rtt * 1000;
        _pingText.text = $"RTT: {rtt}";
    }
}
