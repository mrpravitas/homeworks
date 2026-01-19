using TMPro;
using UnityEngine;

public class TotalTargetDestroyedDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalTargetDestroyedCountText;

    public void UpdateTotalTargetCountUI(int shotsCount)
    {
        _totalTargetDestroyedCountText.text = $"Total target destroyed: {shotsCount}";
    }
}
