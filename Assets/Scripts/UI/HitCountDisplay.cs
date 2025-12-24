using TMPro;
using UnityEngine;

public class HitCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hitsCountTexy;

    public void UpdateHitsCountUI(int hitsCount)
    {
        _hitsCountTexy.text = $"Hits: {hitsCount}";
    }
}
