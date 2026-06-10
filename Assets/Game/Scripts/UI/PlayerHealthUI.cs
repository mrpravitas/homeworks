using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        PlayerHealth.OnHealthChanged += ChangeHeathView;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= ChangeHeathView;
    }

    private void ChangeHeathView(int health)
    {
        _text.text = health.ToString();
    }
}
