using TMPro;
using UnityEngine;

public class BestTargetDestroyedDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestTargetDestroyedCountText;

    public void UpdateBestTargetCountUI(int shotsCount)
    {
        _bestTargetDestroyedCountText.text = $"Best target destroyed: {shotsCount}";
    }
}
