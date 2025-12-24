using TMPro;
using UnityEngine;

public class ShotCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _shotsCountText;

    public void UpdateSotsCountUI(int shotsCount)
    {
        _shotsCountText.text = $"Shots: {shotsCount}";
    }
}
