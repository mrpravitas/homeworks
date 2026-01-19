using TMPro;
using UnityEngine;

public class TotalShotsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalShotsCountText;

    public void UpdateTotalShotsCountUI(int shotsCount)
    {
        _totalShotsCountText.text = $"Total shots: {shotsCount}";
    }
}
