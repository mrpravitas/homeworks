using TMPro;
using UnityEngine;

public class ChestsRemainingUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _label;

    private void OnEnable()
    {
        PlayerCollect.ChestsRemainingChanged += SetChestsRemaining;
    }

    private void OnDisable()
    {
        PlayerCollect.ChestsRemainingChanged -= SetChestsRemaining;
    }

    private void SetChestsRemaining(int remaining)
    {
        _label.text = $"Chests remaining: {remaining}";
    }
}
