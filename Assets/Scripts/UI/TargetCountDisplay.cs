using TMPro;
using UnityEngine;

public class TargetCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _targetCountText;

    public void UpdateTargetCountUI(int targetCount)
    {
        _targetCountText.text = $"Target count: {targetCount}";
    }
}
