using Mirror;
using TMPro;
using UnityEngine;

public class PingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pingText;

    private float _nextUpdate;

    private void Update()
    {
        if (Time.time < _nextUpdate)
        {
            return;
        }

        _nextUpdate = Time.time + 0.5f;
        _pingText.text = $"RTT: {NetworkTime.rtt * 1000}";
    }
}
